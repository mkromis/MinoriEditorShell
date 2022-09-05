//-----------------------------------------------------------------------
// <copyright file="VirtualCanvas.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorShell.VirtualCanvas.Models;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Models;
using MinoriEditorShell.VirtualCanvas.Services;
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

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls
{
    /// <summary>
    /// VirtualCanvas dynamically figures out which children are visible and creates their visuals
    /// and which children are no longer visible (due to scrolling or zooming) and destroys their
    /// visuals.  This helps manage the memory consumption when you have so many objects that creating
    /// all the WPF visuals would take too much memory.
    /// </summary>
    public class MesVirtualCanvas : VirtualizingPanel, IScrollInfo, IMesVirtualCanvasControl
    {
        private System.Windows.Size _viewPortSize;
        private ObservableCollection<IMesVirtualChild> _children;
        private readonly IList<RectangleF> _dirtyRegions = new List<RectangleF>();
        private readonly IList<RectangleF> _visibleRegions = new List<RectangleF>();
        private IDictionary<IMesVirtualChild, int> _visualPositions;
        private int _nodeCollectCycle;

        /// <summary>
        /// Index of children for rendering
        /// </summary>
        public IMesQuadTree<IMesVirtualChild> Index { get; private set; }

        /// <summary>
        /// View property dependency
        /// </summary>
        public static DependencyProperty VirtualChildProperty =
            DependencyProperty.Register("VirtualChild", typeof(IMesVirtualChild), typeof(MesVirtualCanvas));

        /// <summary>
        /// Event to notify of changed Visuals
        /// </summary>
        public event EventHandler<VisualChangeEventArgs> VisualsChanged;

        private delegate void UpdateHandler();

        /// <summary>
        /// Construct empty virtual canvas.
        /// </summary>
        public MesVirtualCanvas()
        {
            Index = new MesQuadTree<IMesVirtualChild>();
            _children = new ObservableCollection<IMesVirtualChild>();
            _children.CollectionChanged += new NotifyCollectionChangedEventHandler(OnChildrenCollectionChanged);

            // Set default back color
            _contentCanvas = new MesContentCanvas { Background = System.Windows.Media.Brushes.White };

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
        public void ShowQuadTree(bool drawing)
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
        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => RebuildVisuals();

        /// <summary>
        /// Returns true if all Visuals have been created for the current scroll position
        /// and there is no more idle processing needed.
        /// </summary>
        public bool IsDone { get; private set; } = true;

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
                if (e.GetValue(VirtualChildProperty) is IMesVirtualChild n)
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
        public ObservableCollection<IMesVirtualChild> VirtualChildren
        {
            get => _children;
            set
            {
                if (_children != null)
                {
                    _children.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnChildrenCollectionChanged);
                }
                _children = value ?? throw new ArgumentNullException(nameof(value));
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
        /// <param name="child">Child to add</param>
        public void AddVirtualChild(IMesVirtualChild child) => _children.Add(child);

        /// <summary>
        /// Return the list of virtual children that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>The list of virtual children found or null if there are none</returns>
        public IEnumerable<IMesVirtualChild> GetChildrenIntersecting(RectangleF bounds) => Index != null ? Index.GetNodesInside(bounds) : null;

        /// <summary>
        /// Return true if there are any virtual children inside the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>True if a node is found whose bounds intersect the given bounds</returns>
        public bool HasChildrenIntersecting(RectangleF bounds) => Index != null ? Index.HasNodesInside(bounds) : false;

        /// <summary>
        /// The number of visual children that are visible right now.
        /// </summary>
        public int LiveVisualCount => _contentCanvas.Children.Count - 1;

        /// <summary>
        /// Callback whenever the current TranslateTransform is changed.
        /// </summary>
        /// <param name="sender">TranslateTransform</param>
        /// <param name="e">noop</param>
        private void OnTranslateChanged(object sender, EventArgs e) => OnScrollChanged();

        /// <summary>
        /// Callback whenever the current ScaleTransform is changed.
        /// </summary>
        /// <param name="sender">ScaleTransform</param>
        /// <param name="e">noop</param>
        private void OnScaleChanged(object sender, EventArgs e) => OnScrollChanged();

        /// <summary>
        /// The ContentCanvas that is actually the parent of all the VirtualChildren Visuals.
        /// </summary>
        public IMesContentCanvas ContentCanvas => _contentCanvas;

        /// <summary>
        /// The backdrop is the back most child of the ContentCanvas used for drawing any sort
        /// of background that is guaranteed to fill the ViewPort.
        /// </summary>
        public Border Backdrop { get; }

        /// <summary>
        /// Calculate the size needed to display all the virtual children.
        /// </summary>
        private void CalculateExtent()
        {
            if (_children.Count == 0)
            {
                _contentCanvas.Width = 0;
                _contentCanvas.Height = 0;
                Backdrop.Width = 0;
                Backdrop.Height = 0;
                return;
            }

            bool rebuild = false;
            if (Index == null || Extent.Width == 0 || Extent.Height == 0 ||
                double.IsNaN(Extent.Width) || double.IsNaN(Extent.Height))
            {
                rebuild = true;
                _visualPositions = new Dictionary<IMesVirtualChild, int>();

                //Boolean first = true;
                int index = 0;
                foreach (IMesVirtualChild c in _children)
                {
                    _visualPositions[c] = index++;

                    // Sanity check
                    RectangleF childBounds = c.Bounds;
                    if (childBounds.Width != 0 && childBounds.Height != 0)
                    {
                        if (double.IsNaN(childBounds.Width) || double.IsNaN(childBounds.Height))
                        {
                            throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture,
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
                Index = new MesQuadTree<IMesVirtualChild>
                {
                    Bounds = new RectangleF(0, 0, Extent.Width, Extent.Height)
                };
                foreach (IMesVirtualChild n in _children)
                {
                    if (n.Bounds.Width > 0 && n.Bounds.Height > 0)
                    {
                        Index.Insert(n, n.Bounds);
                    }
                }
            }

            // Make sure we honor the min width & height.
            double w = Math.Max(_contentCanvas.MinWidth, Extent.Width);
            double h = Math.Max(_contentCanvas.MinHeight, Extent.Height);
            _contentCanvas.Width = w;
            _contentCanvas.Height = h;

            // Make sure the backdrop covers the ViewPort bounds.
            double zoom = Scale.ScaleX;
            if (!double.IsInfinity(ViewportWidth) &&
                !double.IsInfinity(ViewportHeight))
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
                if (child.GetValue(VirtualChildProperty) is IMesVirtualChild n)
                {
                    SizeF boundSize = n.Bounds.Size;
                    child.Measure(new System.Windows.Size(boundSize.Width, boundSize.Height));
                }
            }
            if (double.IsInfinity(availableSize.Width))
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

        private DispatcherTimer _timer;

        /// <summary>
        /// Begin a timer for lazily creating IVirtualChildren visuals
        /// </summary>
        private void StartLazyUpdate()
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
        private void OnStartLazyUpdate(object sender, EventArgs args)
        {
            _timer.Stop();
            LazyUpdateVisuals();
        }

        /// <summary>
        /// Set the viewport size and raize a scroll changed event.
        /// </summary>
        /// <param name="s">The new size</param>
        private void SetViewportSize(System.Windows.Size s)
        {
            if (s != _viewPortSize)
            {
                _viewPortSize = s;
                OnScrollChanged();
            }
        }

        private int _createQuanta = 1000;
        private int _removeQuanta = 2000;
        private int _gcQuanta = 5000;
        private const int _idealDuration = 50; // 50 milliseconds.
        private readonly MesContentCanvas _contentCanvas;
        private int _added;
        private RectangleF _visible = RectangleF.Empty;

        private delegate int QuantizedWorkHandler(int quantum);

        /// <summary>
        /// Do a quantized unit of work for creating newly visible visuals, and cleaning up visuals that are no
        /// longer needed.
        /// </summary>
        private void LazyUpdateVisuals()
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

            VisualsChanged?.Invoke(this, new VisualChangeEventArgs() { Added = _added, Removed = Removed});

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
        private static int SelfThrottlingWorker(int quantum, int idealDuration, QuantizedWorkHandler handler)
        {
            MesPerfTimer timer = new ();
            timer.Start();
            int count = handler(quantum);

            timer.Stop();
            long duration = timer.GetDuration();

            if (duration > 0 && count > 0)
            {
                long estimatedFullDuration = duration * (quantum / count);
                long newQuanta = quantum * idealDuration / estimatedFullDuration;
                quantum = Math.Max(100, (int)Math.Min(newQuanta, int.MaxValue));
            }

            return quantum;
        }

        /// <summary>
        /// Create visuals for the nodes that are now visible.
        /// </summary>
        /// <param name="quantum">Amount of work we can do here</param>
        /// <returns>Amount of work we did</returns>
        private int LazyCreateNodes(int quantum)
        {
            if (_visible == RectangleF.Empty)
            {
                _visible = GetVisibleRect();
                _visibleRegions.Add(_visible);
                IsDone = false;
            }

            int count = 0;
            int regionCount = 0;
            while (_visibleRegions.Count > 0 && count < quantum)
            {
                RectangleF r = _visibleRegions[0];
                _visibleRegions.RemoveAt(0);
                regionCount++;

                if (Index is { Root: { } })
                {
                    // Iterate over the visible range of nodes and make sure they have visuals.
                    foreach (IMesVirtualChild n in Index.GetNodesInside(r))
                    {
                        if (n.Visual == null)
                        {
                            EnsureVisual(n);
                            _added++;
                        }

                        count++;

                        if (count < quantum)
                        {
                            continue;
                        }

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
        public void EnsureVisual(IMesVirtualChild child)
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
                int position = _visualPositions[child];

                // Now do a binary search for the correct insertion position based
                // on the visual positions of the existing visible children.
                UIElementCollection c = _contentCanvas.Children;
                int min = 0;
                int max = c.Count - 1;
                while (max > min + 1)
                {
                    int i = (min + max) / 2;
                    UIElement v = _contentCanvas.Children[i];
                    if (v.GetValue(VirtualChildProperty) is IMesVirtualChild n)
                    {
                        int index = _visualPositions[n];
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
                    if (!(v.GetValue(VirtualChildProperty) is IMesVirtualChild maxchild) || position > _visualPositions[maxchild])
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
            double minWidth = SmallScrollIncrement.Width * 2;
            double minHeight = SmallScrollIncrement.Height * 2;

            if (r.Width > r.Height && r.Height > minHeight)
            {
                // horizontal slices
                float h = r.Height / 2;
                regions.Add(new RectangleF(r.Left, r.Top, r.Width, h + 10));
                regions.Add(new RectangleF(r.Left, r.Top + h, r.Width, h + 10));
            }
            else if (r.Width < r.Height && r.Width > minWidth)
            {
                // vertical slices
                float w = r.Width / 2;
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
        private int LazyRemoveNodes(int quantum)
        {
            RectangleF visible = GetVisibleRect();
            int count = 0;

            // Also remove nodes that are no longer visible.
            int regionCount = 0;
            while (_dirtyRegions.Count > 0 && count < quantum)
            {
                int last = _dirtyRegions.Count - 1;
                RectangleF dirty = _dirtyRegions[last];
                _dirtyRegions.RemoveAt(last);
                regionCount++;

                if (Index != null)
                {
                    // Iterate over the visible range of nodes and make sure they have visuals.
                    foreach (IMesVirtualChild n in Index.GetNodesInside(dirty))
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
        private int LazyGarbageCollectNodes(int quantum)
        {
            int count = 0;
            // Now after every update also do a full incremental scan over all the children
            // to make sure we didn't leak any nodes that need to be removed.
            while (count < quantum && _nodeCollectCycle < _contentCanvas.Children.Count)
            {
                UIElement e = _contentCanvas.Children[_nodeCollectCycle++];
                if (e.GetValue(VirtualChildProperty) is IMesVirtualChild n)
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
        public bool CanHorizontallyScroll { get; set; } = false;

        /// <summary>
        /// Return whether we are allowed to scroll vertically.
        /// </summary>
        public bool CanVerticallyScroll { get; set; } = false;

        /// <summary>
        /// The height of the canvas to be scrolled.
        /// </summary>
        public double ExtentHeight => Extent.Height * Scale.ScaleY;

        /// <summary>
        /// The width of the canvas to be scrolled.
        /// </summary>
        public double ExtentWidth => Extent.Width * Scale.ScaleX;

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
            //if (_contentCanvas.Zoom != null && visual != this)
            //{
            //    return _zoom.ScrollIntoView(visual as FrameworkElement);
            //}
            //return rectangle;
            throw new NotImplementedException();
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
        public void SetHorizontalOffset(double offset)
        {
            double xoffset = Math.Max(Math.Min(offset, ExtentWidth - ViewportWidth), 0);
            Translate.X = -xoffset;
            OnScrollChanged();
        }

        /// <summary>
        /// Scroll to the given absolute vertical scroll position.
        /// </summary>
        /// <param name="offset">The vertical position to scroll to</param>
        public void SetVerticalOffset(double offset)
        {
            double yoffset = Math.Max(Math.Min(offset, ExtentHeight - ViewportHeight), 0);
            Translate.Y = -yoffset;
            OnScrollChanged();
        }

        /// <summary>
        /// Get the current horizontal scroll position.
        /// </summary>
        public double HorizontalOffset => -Translate.X;

        /// <summary>
        /// Return the current vertical scroll position.
        /// </summary>
        public double VerticalOffset => -Translate.Y;

        /// <summary>
        /// Return the height of the current viewport that is visible in the ScrollViewer.
        /// </summary>
        public double ViewportHeight => _viewPortSize.Height;

        /// <summary>
        /// Return the width of the current viewport that is visible in the ScrollViewer.
        /// </summary>
        public double ViewportWidth => _viewPortSize.Width;

        public SizeF SmallScrollIncrement1 { get; set; } = new SizeF(10, 10);
        public int Removed { get; set; }

        #endregion IScrollInfo Members

        /// <summary>
        /// Get the currently visible rectangle according to current scroll position and zoom factor and
        /// size of scroller viewport.
        /// </summary>
        /// <returns>A rectangle</returns>
        private RectangleF GetVisibleRect()
        {
            // Add a bit of extra around the edges so we are sure to create nodes that have a tiny bit showing.
            float xstart = (float)((HorizontalOffset - SmallScrollIncrement1.Width) / Scale.ScaleX);
            float ystart = (float)((VerticalOffset - SmallScrollIncrement1.Height) / Scale.ScaleY);
            float xend = (float)((HorizontalOffset + (_viewPortSize.Width + (2 * SmallScrollIncrement1.Width))) / Scale.ScaleX);
            float yend = (float)((VerticalOffset + (_viewPortSize.Height + (2 * SmallScrollIncrement1.Height))) / Scale.ScaleY);
            return new RectangleF(xstart, ystart, xend - xstart, yend - ystart);
        }

        /// <summary>
        /// The visible region has changed, so we need to queue up work for dirty regions and new visible regions
        /// then start asynchronously building new visuals via StartLazyUpdate.
        /// </summary>
        private void OnScrollChanged()
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

        /// <summary>
        /// A simple helper to use default implementation
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public void UseDefaultControls(IMesVirtualCanvas dc)
        {
            IMesContentCanvas canvas = ContentCanvas;
            dc.Zoom = new MesMapZoom(canvas);
            dc.Pan = new MesPan(canvas, dc.Zoom);
            dc.AutoScroll = new MesAutoScroll(canvas, dc.Zoom);
            dc.RectZoom = new MesRectangleSelectionGesture(canvas, dc.Zoom);
            dc.Graph = this;
        }
    }
}