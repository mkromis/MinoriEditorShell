//-----------------------------------------------------------------------
// <copyright file="VirtualCanvas.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Models;
using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorStudio.VirtualCanvas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls
{
    public class VisualChangeEventArgs : EventArgs
    {
        public Int32 Added { get; set; }
        public Int32 Removed { get; set; }
        public VisualChangeEventArgs(Int32 added, Int32 removed)
        {
            Added = added;
            Removed = removed;
        }
    }

    /// <summary>
    /// VirtualCanvas dynamically figures out which children are visible and creates their visuals 
    /// and which children are no longer visible (due to scrolling or zooming) and destroys their
    /// visuals.  This helps manage the memory consumption when you have so many objects that creating
    /// all the WPF visuals would take too much memory.
    /// </summary>
    public class VirtualCanvas : VirtualizingPanel, IScrollInfo, IVirtualCanvasControl
    {
        private System.Windows.Size _viewPortSize;
        public QuadTree<IVirtualChild> Index { get; private set; }
        private ObservableCollection<IVirtualChild> _children;
        private readonly IList<RectangleF> _dirtyRegions = new List<RectangleF>();
        private readonly IList<RectangleF> _visibleRegions = new List<RectangleF>();
        private IDictionary<IVirtualChild, Int32> _visualPositions;
        private Int32 _nodeCollectCycle;

        public static DependencyProperty VirtualChildProperty = DependencyProperty.Register("VirtualChild", typeof(IVirtualChild), typeof(VirtualCanvas));

        public event EventHandler<VisualChangeEventArgs> VisualsChanged;

        delegate void UpdateHandler();

        /// <summary>
        /// Construct empty virtual canvas.
        /// </summary>
        public VirtualCanvas()
        {
            Index = new QuadTree<IVirtualChild>();
            _children = new ObservableCollection<IVirtualChild>();
            _children.CollectionChanged += new NotifyCollectionChangedEventHandler(OnChildrenCollectionChanged);

            // Set default back color
            _contentCanvas = new ContentCanvas();
            _contentCanvas.Background = System.Windows.Media.Brushes.White;

            // Setup boarder
            Backdrop = new Border();
            _contentCanvas.Children.Add(Backdrop);

            TransformGroup g = new TransformGroup();
            Scale = new ScaleTransform();
            Translate = new TranslateTransform();
            g.Children.Add(Scale);
            g.Children.Add(Translate);
            _contentCanvas.RenderTransform = g;

            Translate.Changed += new EventHandler(OnTranslateChanged);
            Scale.Changed += new EventHandler(OnScaleChanged);
            Children.Add(_contentCanvas);
        }

        /// <summary>
        /// Statitics for internal rendering.
        /// </summary>
        public void ShowQuadTree(Boolean drawing)
        {
            if (drawing)
            {
                Index.ShowQuadTree(_contentCanvas);
            }
            else
            {
                RebuildVisuals();
            }
        }

        /// <summary>
        /// Callback when _children collection is changed.
        /// </summary>
        /// <param name="sender">This</param>
        /// <param name="e">noop</param>
        void OnChildrenCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e) => RebuildVisuals();

        /// <summary>
        /// Get/Set the MapZoom object used for manipulating the scale and translation on this canvas.
        /// </summary>
        public IMapZoom Zoom {
            get => _zoom;
            set => _zoom = (MapZoom)value;
        }

        /// <summary>
        /// Returns true if all Visuals have been created for the current scroll position
        /// and there is no more idle processing needed.
        /// </summary>
        public Boolean IsDone { get; private set; } = true;

        /// <summary>
        /// Resets the state so there is no Visuals associated with this canvas.
        /// </summary>
        private void RebuildVisuals()
        {
            // need to rebuild the index.
            Index = null;
            _visualPositions = null;
            _visible = RectangleF.Empty;
            IsDone = false;
            foreach (UIElement e in _contentCanvas.Children)
            {
                if (e.GetValue(VirtualChildProperty) is IVirtualChild n)
                {
                    e.ClearValue(VirtualChildProperty);
                    n.DisposeVisual();
                }
            }
            _contentCanvas.Children.Clear();
            _contentCanvas.Children.Add(Backdrop);
            InvalidateArrange();
            StartLazyUpdate();
        }

        /// <summary>
        /// The current zoom transform.
        /// </summary>
        public ScaleTransform Scale { get; }

        /// <summary>
        /// The current translate transform.
        /// </summary>
        public TranslateTransform Translate { get; }

        /// <summary>
        /// Get/Set the IVirtualChild collection.  The VirtualCanvas will call CreateVisual on them
        /// when the Bounds of your child intersects the current visible view port.
        /// </summary>
        public ObservableCollection<IVirtualChild> VirtualChildren
        {
            get => _children;
            set
            {
                if (_children != null)
                {
                    _children.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnChildrenCollectionChanged);
                }
                _children = value ?? throw new ArgumentNullException("value");
                _children.CollectionChanged += new NotifyCollectionChangedEventHandler(OnChildrenCollectionChanged);
                RebuildVisuals();
            }
        }

        /// <summary>
        /// Set the scroll amount for the scroll bar arrows.
        /// </summary>
        public SizeF SmallScrollIncrement
        {
            get => SmallScrollIncrement1;
            set => SmallScrollIncrement1 = value;
        }

        /// <summary>
        /// Add a new IVirtualChild.  The VirtualCanvas will call CreateVisual on them
        /// when the Bounds of your child intersects the current visible view port.
        /// </summary>
        /// <param name="c"></param>
        public void AddVirtualChild(IVirtualChild child) => _children.Add(child);

        /// <summary>
        /// Return the list of virtual children that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>The list of virtual children found or null if there are none</returns>
        public IEnumerable<IVirtualChild> GetChildrenIntersecting(RectangleF bounds) => Index != null ? Index.GetNodesInside(bounds) : null;

        /// <summary>
        /// Return true if there are any virtual children inside the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>True if a node is found whose bounds intersect the given bounds</returns>
        public Boolean HasChildrenIntersecting(RectangleF bounds) => Index != null ? Index.HasNodesInside(bounds) : false;

        /// <summary>
        /// The number of visual children that are visible right now.
        /// </summary>
        public Int32 LiveVisualCount => _contentCanvas.Children.Count - 1;

        /// <summary>
        /// Callback whenever the current TranslateTransform is changed.
        /// </summary>
        /// <param name="sender">TranslateTransform</param>
        /// <param name="e">noop</param>
        void OnTranslateChanged(Object sender, EventArgs e) => OnScrollChanged();

        /// <summary>
        /// Callback whenever the current ScaleTransform is changed.
        /// </summary>
        /// <param name="sender">ScaleTransform</param>
        /// <param name="e">noop</param>
        void OnScaleChanged(Object sender, EventArgs e) => OnScrollChanged();

        /// <summary>
        /// The ContentCanvas that is actually the parent of all the VirtualChildren Visuals.
        /// </summary>
        public IContentCanvas ContentCanvas => _contentCanvas;

        /// <summary>
        /// The backgrop is the back most child of the ContentCanvas used for drawing any sort
        /// of background that is guarenteed to fill the ViewPort.
        /// </summary>
        public Border Backdrop { get; }

        /// <summary>
        /// Calculate the size needed to display all the virtual children.
        /// </summary>
        void CalculateExtent()
        {
            if (_children.Count() == 0)
            {
                _contentCanvas.Width = 0;
                _contentCanvas.Height = 0;
                Backdrop.Width = 0;
                Backdrop.Height = 0;
                return;
            }

            Boolean rebuild = false;
            if (Index == null || Extent.Width == 0 || Extent.Height == 0 ||
                Double.IsNaN(Extent.Width) || Double.IsNaN(Extent.Height))
            {
                rebuild = true;
                _visualPositions = new Dictionary<IVirtualChild, Int32>();

                //Boolean first = true;
                Int32 index = 0;
                foreach (IVirtualChild c in _children)
                {
                    _visualPositions[c] = index++;

                    // Sanity check
                    RectangleF childBounds = c.Bounds;
                    if (childBounds.Width != 0 && childBounds.Height != 0)
                    {
                        if (Double.IsNaN(childBounds.Width) || Double.IsNaN(childBounds.Height))
                        {
                            throw new InvalidOperationException(String.Format(CultureInfo.CurrentUICulture,
                                "Child type '{0}' returned NaN bounds", c.GetType().Name));
                        }
                    }

                    // This is an expensive solution, need to re-visit this later.
                    c.BoundsChanged += (s, e) =>
                    {
                        RebuildVisuals();
                    };
                }

                // Get extents
                Extent = new SizeF(
                    _children.Max(x => x.Bounds.Right),
                    _children.Max(x => x.Bounds.Bottom));

                // Ok, now we know the size we can create the index.
                Index = new QuadTree<IVirtualChild>
                {
                    Bounds = new RectangleF(0, 0, Extent.Width, Extent.Height)
                };
                foreach (IVirtualChild n in _children)
                {
                    if (n.Bounds.Width > 0 && n.Bounds.Height > 0)
                    {
                        Index.Insert(n, n.Bounds);
                    }
                }
            }

            // Make sure we honor the min width & height.
            Double w = Math.Max(_contentCanvas.MinWidth, Extent.Width);
            Double h = Math.Max(_contentCanvas.MinHeight, Extent.Height);
            _contentCanvas.Width = w;
            _contentCanvas.Height = h;

            // Make sure the backdrop covers the ViewPort bounds.
            Double zoom = Scale.ScaleX;
            if (!Double.IsInfinity(ViewportHeight) &&
                !Double.IsInfinity(ViewportHeight))
            {
                w = Math.Max(w, ViewportWidth / zoom);
                h = Math.Max(h, ViewportHeight / zoom);
                Backdrop.Width = w;
                Backdrop.Height = h;
            }

            if (ScrollOwner != null)
            {
                ScrollOwner.InvalidateScrollInfo();
            }

            if (rebuild)
            {
                AddVisibleRegion();
            }
        }


        /// <summary>
        /// WPF Measure override for measuring the control
        /// </summary>
        /// <param name="availableSize">Available size will be the viewport size in the scroll viewer</param>
        /// <returns>availableSize</returns>
        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            base.MeasureOverride(availableSize);

            // We will be given the visible size in the scroll viewer here.
            CalculateExtent();

            if (availableSize.Width != _viewPortSize.Width && availableSize.Height != _viewPortSize.Height)
            {
                SetViewportSize(availableSize);
            }

            foreach (UIElement child in InternalChildren)
            {
                if (child.GetValue(VirtualChildProperty) is IVirtualChild n)
                {
                    SizeF boundSize = n.Bounds.Size;
                    child.Measure(new System.Windows.Size(boundSize.Width, boundSize.Height));
                }
            }
            if (Double.IsInfinity(availableSize.Width))
            {
                return new System.Windows.Size(Extent.Width, Extent.Height);
            }
            else
            {
                return availableSize;
            }
        }

        /// <summary>
        /// WPF ArrangeOverride for laying out the control
        /// </summary>
        /// <param name="finalSize">The size allocated by parents</param>
        /// <returns>finalSize</returns>
        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {
            base.ArrangeOverride(finalSize);

            CalculateExtent();

            if (finalSize != _viewPortSize)
            {
                SetViewportSize(finalSize);
            }

            _contentCanvas.Arrange(new Rect(0, 0, _contentCanvas.Width, _contentCanvas.Height));

            if (Index == null)
            {
                StartLazyUpdate();
            }

            return finalSize;
        }

        DispatcherTimer _timer;

        /// <summary>
        /// Begin a timer for lazily creating IVirtualChildren visuals
        /// </summary>
        void StartLazyUpdate()
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(10), DispatcherPriority.Normal,
                    new EventHandler(OnStartLazyUpdate), Dispatcher);
            }
            _timer.Start();
        }

        /// <summary>
        /// Callback from the DispatchTimer
        /// </summary>
        /// <param name="sender">DispatchTimer </param>
        /// <param name="args">noop</param>
        void OnStartLazyUpdate(Object sender, EventArgs args)
        {
            _timer.Stop();
            LazyUpdateVisuals();
        }

        /// <summary>
        /// Set the viewport size and raize a scroll changed event.
        /// </summary>
        /// <param name="s">The new size</param>
        void SetViewportSize(System.Windows.Size s)
        {
            if (s != _viewPortSize)
            {
                _viewPortSize = s;
                OnScrollChanged();
            }
        }

        Int32 _createQuanta = 1000;
        Int32 _removeQuanta = 2000;
        Int32 _gcQuanta = 5000;
        readonly Int32 _idealDuration = 50; // 50 milliseconds.
        private readonly ContentCanvas _contentCanvas;
        Int32 _added;
        RectangleF _visible = RectangleF.Empty;
        private MapZoom _zoom;

        delegate Int32 QuantizedWorkHandler(Int32 quantum);

        /// <summary>
        /// Do a quantized unit of work for creating newly visible visuals, and cleaning up visuals that are no
        /// longer needed.
        /// </summary>
        void LazyUpdateVisuals()
        {
            if (Index == null)
            {
                CalculateExtent();
            }

            IsDone = true;
            _added = 0;
            Removed = 0;

            _createQuanta = SelfThrottlingWorker(_createQuanta, _idealDuration, new QuantizedWorkHandler(LazyCreateNodes));
            _removeQuanta = SelfThrottlingWorker(_removeQuanta, _idealDuration, new QuantizedWorkHandler(LazyRemoveNodes));
            _gcQuanta = SelfThrottlingWorker(_gcQuanta, _idealDuration, new QuantizedWorkHandler(LazyGarbageCollectNodes));

            VisualsChanged?.Invoke(this, new VisualChangeEventArgs(_added, Removed));

            if (_added > 0)
            {
                InvalidateArrange();
            }
            if (!IsDone)
            {
                StartLazyUpdate();
                //this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new UpdateHandler(LazyUpdateVisuals));
            }
            InvalidateVisual();
        }

        /// <summary>
        /// Helper method for self-tuning how much time is allocated to the given handler.
        /// </summary>
        /// <param name="quantum">The current quantum allocation</param>
        /// <param name="idealDuration">The time in milliseconds we want to take</param>
        /// <param name="handler">The handler to call that does the work being throttled</param>
        /// <returns>Returns the new quantum to use next time that will more likely hit the ideal time</returns>
        private static Int32 SelfThrottlingWorker(Int32 quantum, Int32 idealDuration, QuantizedWorkHandler handler)
        {
            PerfTimer timer = new PerfTimer();
            timer.Start();
            Int32 count = handler(quantum);

            timer.Stop();
            Int64 duration = timer.GetDuration();

            if (duration > 0 && count > 0)
            {
                Int64 estimatedFullDuration = duration * (quantum / count);
                Int64 newQuanta = (quantum * idealDuration) / estimatedFullDuration;
                quantum = Math.Max(100, (Int32)Math.Min(newQuanta, Int32.MaxValue));
            }

            return quantum;
        }

        /// <summary>
        /// Create visuals for the nodes that are now visible.
        /// </summary>
        /// <param name="quantum">Amount of work we can do here</param>
        /// <returns>Amount of work we did</returns>
        private Int32 LazyCreateNodes(Int32 quantum)
        {

            if (_visible == RectangleF.Empty)
            {
                _visible = GetVisibleRect();
                _visibleRegions.Add(_visible);
                IsDone = false;
            }

            Int32 count = 0;
            Int32 regionCount = 0;
            while (_visibleRegions.Count > 0 && count < quantum)
            {
                RectangleF r = _visibleRegions[0];
                _visibleRegions.RemoveAt(0);
                regionCount++;

                if (Index != null && Index.Root != null)
                {
                    // Iterate over the visible range of nodes and make sure they have visuals.
                    foreach (IVirtualChild n in Index.GetNodesInside(r))
                    {
                        if (n.Visual == null)
                        {
                            EnsureVisual(n);
                            _added++;
                        }

                        count++;

                        if (count >= quantum)
                        {
                            // This region is too big, so subdivide it into smaller slices.
                            if (regionCount == 1)
                            {
                                // We didn't even complete 1 region, so we better split it.
                                SplitRegion(r, _visibleRegions);
                            }
                            else
                            {
                                _visibleRegions.Add(r); // put it back since we're not done!
                            }
                            IsDone = false;
                            break;
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Insert the visual for the child in the same order as is is defined in the 
        /// VirtualChildren collection so the visuals draw on top of each other in the expected order.
        /// The trick is that GetNodesIntersecting returns the nodes in pretty much random order depending 
        /// on how the QuadTree decides to break up the canvas.  
        /// 
        /// The thing we should avoid is a linear search through the potentially large collection of 
        /// IVirtualChildren to compute its visible index which is why we have the _visualPositions map.  
        /// We should also avoid a N*M algorithm where N is the number of nodes returned from GetNodesIntersecting 
        /// and M is the number of children already visible.  For example, Page down in a zoomed out situation 
        /// gives potentially high N and and M which would basically be an O(n2) algorithm.  
        /// 
        /// So the solution is to use the _visualPositions map to get the expected visual position index
        /// of a given IVirtualChild, then do a binary search through existing visible children to find the
        /// insertion point of the new child.  So this is O(Log M).  
        /// </summary>
        /// <param name="child">The IVirtualChild to add visual for</param>
        public void EnsureVisual(IVirtualChild child)
        {
            if (child.Visual != null)
            {
                return;
            }

            if (child.CreateVisual(this) is UIElement e)
            {
                e.SetValue(VirtualChildProperty, child);
                RectangleF bounds = child.Bounds;
                Canvas.SetLeft(e, bounds.Left);
                Canvas.SetTop(e, bounds.Top);

                // Get the correct absolute position of this child.
                Int32 position = _visualPositions[child];

                // Now do a binary search for the correct insertion position based
                // on the visual positions of the existing visible children.
                UIElementCollection c = _contentCanvas.Children;
                Int32 min = 0;
                Int32 max = c.Count - 1;
                while (max > min + 1)
                {
                    Int32 i = (min + max) / 2;
                    UIElement v = _contentCanvas.Children[i];
                    if (v.GetValue(VirtualChildProperty) is IVirtualChild n)
                    {
                        Int32 index = _visualPositions[n];
                        if (index > position)
                        {
                            // search from min to i.
                            max = i;
                        }
                        else
                        {
                            // search from i to max.
                            min = i;
                        }
                    }
                    else
                    {
                        // Any nodes without IVirtualChild should be behind the
                        // IVirtualChildren by definition (like the Backdrop).
                        min = i;
                    }
                }

                // If 'max' is the last child in the collection, then we need to see
                // if we have a new last child.
                if (max == c.Count - 1)
                {
                    UIElement v = c[max];
                    if (!(v.GetValue(VirtualChildProperty) is IVirtualChild maxchild) || position > _visualPositions[maxchild])
                    {
                        // Then we have a new last child!
                        max++;
                    }
                }

                c.Insert(max, e);
            }
        }


        /// <summary>
        /// Split a rectangle into 2 and add them to the regions list.
        /// </summary>
        /// <param name="r">Rectangle to split</param>
        /// <param name="regions">List to add to</param>
        private void SplitRegion(RectangleF r, IList<RectangleF> regions)
        {
            Double minWidth = SmallScrollIncrement.Width * 2;
            Double minHeight = SmallScrollIncrement.Height * 2;

            if (r.Width > r.Height && r.Height > minHeight)
            {
                // horizontal slices
                Single h = r.Height / 2;
                regions.Add(new RectangleF(r.Left, r.Top, r.Width, h + 10));
                regions.Add(new RectangleF(r.Left, r.Top + h, r.Width, h + 10));
            }
            else if (r.Width < r.Height && r.Width > minWidth)
            {
                // vertical slices
                Single w = r.Width / 2;
                regions.Add(new RectangleF(r.Left, r.Top, w + 10, r.Height));
                regions.Add(new RectangleF(r.Left + w, r.Top, w + 10, r.Height));
            }
            else
            {
                regions.Add(r); // put it back since we're not done!
            }
        }

        /// <summary>
        /// Remove visuals for nodes that are no longer visible.
        /// </summary>
        /// <param name="quantum">Amount of work we can do here</param>
        /// <returns>Amount of work we did</returns>
        private Int32 LazyRemoveNodes(Int32 quantum)
        {
            RectangleF visible = GetVisibleRect();
            Int32 count = 0;

            // Also remove nodes that are no longer visible.
            Int32 regionCount = 0;
            while (_dirtyRegions.Count > 0 && count < quantum)
            {
                Int32 last = _dirtyRegions.Count - 1;
                RectangleF dirty = _dirtyRegions[last];
                _dirtyRegions.RemoveAt(last);
                regionCount++;

                if (Index != null)
                {
                    // Iterate over the visible range of nodes and make sure they have visuals.
                    foreach (IVirtualChild n in Index.GetNodesInside(dirty))
                    {
                        if (n.Visual is UIElement e)
                        {
                            RectangleF nrect = n.Bounds;
                            if (!nrect.IntersectsWith(visible))
                            {
                                e.ClearValue(VirtualChildProperty);
                                _contentCanvas.Children.Remove(e);
                                n.DisposeVisual();
                                Removed++;
                            }
                        }

                        count++;
                        if (count >= quantum)
                        {
                            if (regionCount == 1)
                            {
                                // We didn't even complete 1 region, so we better split it.
                                SplitRegion(dirty, _dirtyRegions);
                            }
                            else
                            {
                                _dirtyRegions.Add(dirty); // put it back since we're not done!
                            }
                            IsDone = false;
                            break;
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Check all child nodes to see if any leaked from LazyRemoveNodes and remove their visuals.
        /// </summary>
        /// <param name="quantum">Amount of work we can do here</param>
        /// <returns>The amount of work we did</returns>
        Int32 LazyGarbageCollectNodes(Int32 quantum)
        {

            Int32 count = 0;
            // Now after every update also do a full incremental scan over all the children
            // to make sure we didn't leak any nodes that need to be removed.
            while (count < quantum && _nodeCollectCycle < _contentCanvas.Children.Count)
            {
                UIElement e = _contentCanvas.Children[_nodeCollectCycle++];
                if (e.GetValue(VirtualChildProperty) is IVirtualChild n)
                {
                    RectangleF nrect = n.Bounds;
                    if (!nrect.IntersectsWith(_visible))
                    {
                        e.ClearValue(VirtualChildProperty);
                        _contentCanvas.Children.Remove(e);
                        n.DisposeVisual();
                        Removed++;
                    }
                    count++;
                }
                _nodeCollectCycle++;
            }

            if (_nodeCollectCycle < _contentCanvas.Children.Count)
            {
                IsDone = false;
            }

            return count;
        }

        /// <summary>
        /// Return the full size of this canvas.
        /// </summary>
        public SizeF Extent { get; private set; }

        #region IScrollInfo Members

        /// <summary>
        /// Return whether we are allowed to scroll horizontally.
        /// </summary>
        public Boolean CanHorizontallyScroll { get; set; } = false;

        /// <summary>
        /// Return whether we are allowed to scroll vertically.
        /// </summary>
        public Boolean CanVerticallyScroll { get; set; } = false;

        /// <summary>
        /// The height of the canvas to be scrolled.
        /// </summary>
        public Double ExtentHeight => Extent.Height * Scale.ScaleY;

        /// <summary>
        /// The width of the canvas to be scrolled.
        /// </summary>
        public Double ExtentWidth => Extent.Width * Scale.ScaleX;

        /// <summary>
        /// Scroll down one small scroll increment.
        /// </summary>
        public void LineDown() => SetVerticalOffset(VerticalOffset + (SmallScrollIncrement1.Height * Scale.ScaleX));

        /// <summary>
        /// Scroll left by one small scroll increment.
        /// </summary>
        public void LineLeft() => SetHorizontalOffset(HorizontalOffset - (SmallScrollIncrement1.Width * Scale.ScaleX));

        /// <summary>
        /// Scroll right by one small scroll increment
        /// </summary>
        public void LineRight() => SetHorizontalOffset(HorizontalOffset + (SmallScrollIncrement1.Width * Scale.ScaleX));

        /// <summary>
        /// Scroll up by one small scroll increment
        /// </summary>
        public void LineUp() => SetVerticalOffset(VerticalOffset - (SmallScrollIncrement1.Height * Scale.ScaleX));

        /// <summary>
        /// Make the given visual at the given bounds visible.
        /// </summary>
        /// <param name="visual">The visual that will become visible</param>
        /// <param name="rectangle">The bounds of that visual</param>
        /// <returns>The bounds that is actually visible.</returns>
        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            if (Zoom != null && visual != this)
            {
                return _zoom.ScrollIntoView(visual as FrameworkElement);
            }
            return rectangle;
        }

        /// <summary>
        /// Scroll down by one mouse wheel increment.
        /// </summary>
        public void MouseWheelDown() => SetVerticalOffset(VerticalOffset + (SmallScrollIncrement1.Height * Scale.ScaleX));

        /// <summary>
        /// Scroll left by one mouse wheel increment.
        /// </summary>
        public void MouseWheelLeft() => SetHorizontalOffset(HorizontalOffset + (SmallScrollIncrement1.Width * Scale.ScaleX));

        /// <summary>
        /// Scroll right by one mouse wheel increment.
        /// </summary>
        public void MouseWheelRight() => SetHorizontalOffset(HorizontalOffset - (SmallScrollIncrement1.Width * Scale.ScaleX));

        /// <summary>
        /// Scroll up by one mouse wheel increment.
        /// </summary>
        public void MouseWheelUp() => SetVerticalOffset(VerticalOffset - (SmallScrollIncrement1.Height * Scale.ScaleX));

        /// <summary>
        /// Page down by one view port height amount.
        /// </summary>
        public void PageDown() => SetVerticalOffset(VerticalOffset + _viewPortSize.Height);

        /// <summary>
        /// Page left by one view port width amount.
        /// </summary>
        public void PageLeft() => SetHorizontalOffset(HorizontalOffset - _viewPortSize.Width);

        /// <summary>
        /// Page right by one view port width amount.
        /// </summary>
        public void PageRight() => SetHorizontalOffset(HorizontalOffset + _viewPortSize.Width);

        /// <summary>
        /// Page up by one view port height amount.
        /// </summary>
        public void PageUp() => SetVerticalOffset(VerticalOffset - _viewPortSize.Height);

        /// <summary>
        /// Return the ScrollViewer that contains this object.
        /// </summary>
        public ScrollViewer ScrollOwner { get; set; }

        /// <summary>
        /// Scroll to the given absolute horizontal scroll position.
        /// </summary>
        /// <param name="offset">The horizontal position to scroll to</param>
        public void SetHorizontalOffset(Double offset)
        {
            Double xoffset = Math.Max(Math.Min(offset, ExtentWidth - ViewportWidth), 0);
            Translate.X = -xoffset;
            OnScrollChanged();
        }

        /// <summary>
        /// Scroll to the given absolute vertical scroll position.
        /// </summary>
        /// <param name="offset">The vertical position to scroll to</param>
        public void SetVerticalOffset(Double offset)
        {
            Double yoffset = Math.Max(Math.Min(offset, ExtentHeight - ViewportHeight), 0);
            Translate.Y = -yoffset;
            OnScrollChanged();
        }

        /// <summary>
        /// Get the current horizontal scroll position.
        /// </summary>
        public Double HorizontalOffset => -Translate.X;

        /// <summary>
        /// Return the current vertical scroll position.
        /// </summary>
        public Double VerticalOffset => -Translate.Y;

        /// <summary>
        /// Return the height of the current viewport that is visible in the ScrollViewer.
        /// </summary>
        public Double ViewportHeight => _viewPortSize.Height;

        /// <summary>
        /// Return the width of the current viewport that is visible in the ScrollViewer.
        /// </summary>
        public Double ViewportWidth => _viewPortSize.Width;

        public SizeF SmallScrollIncrement1 { get; set; } = new SizeF(10, 10);
        public Int32 Removed { get; set; }

        #endregion

        /// <summary>
        /// Get the currently visible rectangle according to current scroll position and zoom factor and
        /// size of scroller viewport.
        /// </summary>
        /// <returns>A rectangle</returns>
        RectangleF GetVisibleRect()
        {
            // Add a bit of extra around the edges so we are sure to create nodes that have a tiny bit showing.
            Single xstart = (Single)((HorizontalOffset - SmallScrollIncrement1.Width) / Scale.ScaleX);
            Single ystart = (Single)((VerticalOffset - SmallScrollIncrement1.Height) / Scale.ScaleY);
            Single xend = (Single)((HorizontalOffset + (_viewPortSize.Width + (2 * SmallScrollIncrement1.Width))) / Scale.ScaleX);
            Single yend = (Single)((VerticalOffset + (_viewPortSize.Height + (2 * SmallScrollIncrement1.Height))) / Scale.ScaleY);
            return new RectangleF(xstart, ystart, xend - xstart, yend - ystart);
        }

        /// <summary>
        /// The visible region has changed, so we need to queue up work for dirty regions and new visible regions
        /// then start asynchronously building new visuals via StartLazyUpdate.
        /// </summary>
        void OnScrollChanged()
        {
            RectangleF dirty = _visible;
            AddVisibleRegion();
            _nodeCollectCycle = 0;
            IsDone = false;

            RectangleF intersection = RectangleF.Intersect(dirty, _visible);
            if (intersection == RectangleF.Empty)
            {
                _dirtyRegions.Add(dirty); // the whole thing is dirty
            }
            else
            {
                // Add left stripe
                if (dirty.Left < intersection.Left)
                {
                    _dirtyRegions.Add(new RectangleF(dirty.Left, dirty.Top, intersection.Left - dirty.Left, dirty.Height));
                }
                // Add right stripe
                if (dirty.Right > intersection.Right)
                {
                    _dirtyRegions.Add(new RectangleF(intersection.Right, dirty.Top, dirty.Right - intersection.Right, dirty.Height));
                }
                // Add top stripe
                if (dirty.Top < intersection.Top)
                {
                    _dirtyRegions.Add(new RectangleF(dirty.Left, dirty.Top, dirty.Width, intersection.Top - dirty.Top));
                }
                // Add right stripe
                if (dirty.Bottom > intersection.Bottom)
                {
                    _dirtyRegions.Add(new RectangleF(dirty.Left, intersection.Bottom, dirty.Width, dirty.Bottom - intersection.Bottom));
                }
            }

            StartLazyUpdate();
            InvalidateScrollInfo();
        }

        /// <summary>
        /// Tell the ScrollViewer to update the scrollbars because, extent, zoom or translate has changed.
        /// </summary>
        public void InvalidateScrollInfo()
        {
            if (ScrollOwner != null)
            {
                ScrollOwner.InvalidateScrollInfo();
            }
        }

        /// <summary>
        /// Add the current visible rect to the list of regions to process
        /// </summary>
        private void AddVisibleRegion()
        {
            _visible = GetVisibleRect();
            _visibleRegions.Clear();
            _visibleRegions.Add(_visible);
        }
    }
}
