//-----------------------------------------------------------------------
// <copyright file="MapZoom.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Services;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures
{
    /// <summary>
    /// The MapZoom gesture provides animated mouse wheel zooming behavior as well as a
    /// general management of the scale and translate transformations and a "ScrollIntoView"
    /// method than can be used by other gestures.
    /// </summary>
    public class MesMapZoom : Animatable, IMesMapZoom
    {
        // sensitivity is a number between 0 and 1 that controls how much each mouse wheel click
        // zooms in.  This value means one mouse click will zoom to 0.90 of the original size, but
        // multiple mouse clicks when you flick the mouse wheel accumulate so it can be more than this.
        // 0.10 feels about right, but you can increase the number if you feel you have to work the
        // mouse wheel to hard to zoom in as fast as you want to.
        private const Double _sensitivity = .10;

        // defaultZoomTime is the amount of time in milliseconds the zoom animation takes to give
        // a smooth zoom in behavior.  This time increases when you flick the mouse wheel up to maxZoomTime.
        private const Double _defaultZoomTime = 100;

        private const Double _maxZoomTime = 300;

        private FrameworkElement _container;
        private FrameworkElement _target;

        // Note that because we want to animate the zoom and translate while spinning the mouse
        // we keep two sets of values.  Note that when an animation is active on a given property it
        // will blow away any value you might try to set programatically on that same property.
        // So the master non-animated values live in the actual Transform objects and anyone can
        // set these values and they will "stick".  The animated values are stored in _zoom and
        // _offset and these values are copied to the master _scale and _translate on each animation
        // step.  If some external change is made to the _scale and _translate transforms then
        // we "StopAnimation" and sync up the _offset and _zoom variables.
        private ScaleTransform _scale;

        private TranslateTransform _translate;
        private Double _value = 1;
        private Double _newZoom = 1;
        private Point _offset;
        private Point _mouse;
        private Point _onTarget;
        private Double _zoomTime = _defaultZoomTime;
        private Int64 _startTime;
        private Double _lastAmount;
        private Rect _targetRect;

        /// <summary>
        /// The offset property that can be animated.
        /// </summary>
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register("Offset", typeof(Point), typeof(MesMapZoom));

        /// <summary>
        /// When we are zooming to a point this property can be animated
        /// </summary>
        public static readonly DependencyProperty ZoomToPointProperty = DependencyProperty.Register("ZoomToPoint", typeof(Double), typeof(MesMapZoom));

        /// <summary>
        /// When we are zooming to a rectangle this property can be animated.
        /// </summary>
        public static readonly DependencyProperty ZoomToRectProperty = DependencyProperty.Register("ZoomToRect", typeof(Double), typeof(MesMapZoom));

        /// <summary>
        /// This event is raised when the scale or translation is changed.
        /// </summary>
        public event EventHandler<Double> ValueChanged;

        /// <summary>
        /// Construct new MapZoom object that manages the RenderTransform of the given target object.
        /// The target object must have a parent container.
        /// </summary>
        /// <param name="target">The target object we will be zooming.</param>
        public MesMapZoom(IMesContentCanvas target) => Initialize((MesContentCanvas)target);

        /// <summary>
        /// Construct new MapZoom object that manages the RenderTransform of the given target object.
        /// The target object must have a parent container.
        /// </summary>
        /// <param name="target">The target object we will be zooming.</param>
        public MesMapZoom(FrameworkElement target) => Initialize(target);

        private void Initialize(FrameworkElement target)
        {
            _container = target.Parent as FrameworkElement;
            _target = target;

            _container.MouseMove += new MouseEventHandler(OnMouseMove);
            _container.MouseWheel += new MouseWheelEventHandler(OnMouseWheel);

            Keyboard.AddKeyDownHandler(_container, new KeyEventHandler(OnKeyDown));
            Keyboard.AddKeyUpHandler(_container, new KeyEventHandler(OnKeyUp));

            _container.Focusable = true;
            _container.Focus();

            // Try and reuse the existing TransformGroup if we can.
            if (target.RenderTransform is TransformGroup g)
            {
                _scale = g.Children.Count > 1 ? g.Children[0] as ScaleTransform : null;
                _translate = g.Children.Count > 0 ? g.Children[1] as TranslateTransform : null;
            }
            else
            {
                g = new TransformGroup();
                _scale = new ScaleTransform(1, 1);
                g.Children.Add(_scale);
                _translate = new TranslateTransform();
                g.Children.Add(_translate);
                target.RenderTransform = g;
            }

            _value = _newZoom = _scale?.ScaleX ?? 0;

            // track changes made by the ScrolLViewer.
            if (_translate != null) _translate.Changed += new EventHandler(OnTranslateChanged);
            if (_scale != null) _scale.Changed += new EventHandler(OnScaleChanged);
        }

        /// <summary>
        /// Handle event when the TranslateTransform is changed by keeping our offset member in sync
        /// the new TranslateTransform values.
        /// </summary>
        /// <param name="sender">The TranslateTransform object</param>
        /// <param name="e">noop</param>
        private void OnTranslateChanged(Object sender, EventArgs e)
        {
            if (_offset.X != _translate.X || _offset.Y != _translate.Y)
            {
                _offset.X = _translate.X;
                _offset.Y = _translate.Y;
            }
        }

        /// <summary>
        /// Handle event when the ScaleTransform is changed by keeping our zoom member in sync with the
        /// new ScaleTransform values.
        /// </summary>
        /// <param name="sender">The ScaleTransform object</param>
        /// <param name="e">noop</param>
        private void OnScaleChanged(Object sender, EventArgs e) => _value = _scale.ScaleX;

        /// <summary>
        /// Handle the key down event and show special cursor when control key is pressed to
        /// indicate we can do some special behavior
        /// </summary>
        /// <param name="sender">Keyboard</param>
        /// <param name="e">Key information</param>
        private void OnKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl)
            {
                _container.Cursor = Cursors.SizeAll;
            }
        }

        /// <summary>
        /// Handle the key up event and remove any special cursor we set in Key down.
        /// </summary>
        /// <param name="sender">Keyboard</param>
        /// <param name="e">Key information</param>
        private void OnKeyUp(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl)
            {
                _container.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Handle mouse move event and record the position since we want the zoom to be centered around
        /// this position.
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Mouse move information</param>
        private void OnMouseMove(Object sender, System.Windows.Input.MouseEventArgs e)
        {
            _mouse = _container.PointToScreen(e.GetPosition(_container));
            _onTarget = e.GetPosition(_target);

            // Create a 50 pixel margin on the edges of the target object so that if the mouse is inside
            // that band, then that edge stays pinned on screen and doesn't slide off the edge as we zoom in.
            if (_onTarget.X < 50 / _value) { _onTarget.X = 0; }
            if (_onTarget.Y < 50 / _value) { _onTarget.Y = 0; }
            if (_onTarget.X + (50 / _value) > _target.ActualWidth) { _onTarget.X = _target.ActualWidth; }
            if (_onTarget.Y + (50 / _value) > _target.ActualHeight) { _onTarget.Y = _target.ActualHeight; }
        }

        /// <summary>
        /// Handle mouse wheel event and do the actuall zooming if the control key is down.
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Mouse wheel information</param>
        public void OnMouseWheel(Object sender, MouseWheelEventArgs e)
        {
            if (e.Delta != 0 && (Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                HandleZoom(e.Delta / (Double)Mouse.MouseWheelDeltaForOneLine);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Apply the new animated _zoom value to the actual ScaleTransform and fire the ZoomChanged event.
        /// </summary>
        private void OnZoomChanged()
        {
            _scale.ScaleX = _scale.ScaleY = _value;

            ValueChanged?.Invoke(this, Value);

            // focus rectangles may need to be repainted.
            _target.InvalidateVisual();
        }

        /// <summary>
        /// Reset current translate offset back to zero and stop any existing zoom/translate animations.
        /// </summary>
        public void ResetTranslate()
        {
            StopAnimations();
            Translate(0, 0);
        }

        /// <summary>
        /// Return the visible size of the target object (the IScrollInfo Viewport dimensions).
        /// </summary>
        public Size ContainerSize
        {
            get
            {
                if (_target is IScrollInfo si)
                {
                    return new Size(si.ViewportWidth, si.ViewportHeight);
                }
                // Basically the visible rect...
                return new Size(_container.ActualWidth, _container.ActualHeight);
            }
        }

        /// <summary>
        /// The ScaleTransform scales around 0,0, but want it to scale around _onTarget position.
        /// So initially when we changed the ScaleTransform the mouse will have appeared to have
        /// slipped a bit on the target object which we don't want, so here we calculate the delta
        /// between the current mouse position and the _onTarget position we want and apply a
        /// Translate correction so the user doesn't see any slippage.
        /// </summary>
        private void KeepPositionStable()
        {
            // Make sure this point remains fixed since it is where the cursor is...
            Point moved;
            try
            {
                moved = _target.PointFromScreen(_mouse);
            }
            catch (Exception)
            {
                return;
            }

            Point delta = new Point(moved.X - _onTarget.X, moved.Y - _onTarget.Y);

            Double x = _translate.X + (delta.X * _value);
            Double y = _translate.Y + (delta.Y * _value);

            Size containerSize = ContainerSize;
            Double width = containerSize.Width;
            Double height = containerSize.Height;

            Double right = (_target.ActualWidth * _value) + x;
            if (right < width && x < 0)
            {
                x += width - right;
            }

            Double bottom = (_target.ActualHeight * _value) + y;
            if (bottom < height && y < 0)
            {
                y += height - bottom;
            }
            Translate(x, y);
        }

        /// <summary>
        /// This version keeps a given rectangle stable by centering it within the visible region
        /// of the target object.
        /// </summary>
        private void KeepRectStable()
        {
            // Find out where we are now that zoom has changed.
            Rect cr = _target.TransformToAncestor(_container).TransformBounds(_targetRect);
            // Keep it centered.
            Size containerSize = ContainerSize;
            Double width = containerSize.Width;
            Double height = containerSize.Height;

            Double cx = (width - cr.Width) / 2;
            Double cy = (height - cr.Height) / 2;
            Double dx = _translate.X + cx - cr.X;
            Double dy = _translate.Y + cy - cr.Y;
            Translate(dx, dy);
        }

        /// <summary>
        /// Translate the target object to the given x-y positions.
        /// </summary>
        /// <param name="x">The x-cordinate</param>
        /// <param name="y">The y-coordinate</param>
        private void Translate(Double x, Double y)
        {
            if (_target is IScrollInfo)
            {
                // If the target object is smaller than the current viewport size then ignore this request.
                Size s = ContainerSize;
                if (_target.ActualWidth <= s.Width && _target.ActualHeight <= s.Height)
                {
                    x = y = 0;
                }
            }
            if (x > 0) { x = 0; }
            if (y > 0) { y = 0; }

            _translate.X = _offset.X = x;
            _translate.Y = _offset.Y = y;

            // focus rectangles may need to be repainted.
            _target.InvalidateVisual();
        }

        /// <summary>
        /// Get/set the current zoom level - this is a scale factor, 0.5 means zoom out so everything is half
        /// the normal size.  A value of 2 means zoom in so everything on the target object is twice the normal size.
        /// </summary>
        public Double Value
        {
            get => _value;
            set
            {
                StopAnimations();
                _scale.ScaleX = _scale.ScaleY = _value = value;
                OnZoomChanged();
            }
        }

        /// <summary>
        /// Get/set the current translation offset.
        /// </summary>
        public Point Offset
        {
            get => _offset;
            set
            {
                StopAnimations();
                Translate(value.X, value.Y);
                _offset = value;
                OnTranslateChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// For operations that need to tweak the zoom or translate coordinates, we must first
        /// stop any animations that are currently changing those values otherwise things get
        /// very confused.
        /// </summary>
        private void StopAnimations()
        {
            // Stop the animation at the current point.
            _newZoom = _value;
            BeginAnimation(ZoomToPointProperty, null);
            BeginAnimation(ZoomToRectProperty, null);

            // make sure offset and translate are in sync.
            Point t = new Point(_translate.X, _translate.Y);
            BeginAnimation(OffsetProperty, null);
            Translate(t.X, t.Y);
        }

        /// <summary>
        /// Overload to assist in platform interface
        /// </summary>
        /// <param name="rectangle"></param>
        public void ZoomToRect(System.Drawing.RectangleF rectangle)
        {
            Rect r = new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            ZoomToRect(r);
        }

        /// <summary>
        /// Animate a zoom out and translate scroll so that the given rectangle is entirely visible.
        /// </summary>
        /// <param name="r">Given rectangle is in "target" coordinates.</param>
        public void ZoomToRect(Rect r)
        {
            StopAnimations();
            // Convert it to container coordinates
            Rect cr = _target.TransformToAncestor(_container).TransformBounds(r);

            Size containerSize = ContainerSize;
            Double width = containerSize.Width;
            Double height = containerSize.Height;

            Double xzoom = width / cr.Width;
            Double yzoom = height / cr.Height;
            Double zoom = _value * Math.Min(xzoom, yzoom);

            Double oldZoom = _value;

            _targetRect = r;
            _startTime = 0;
            AnimateZoom(ZoomToRectProperty, oldZoom, zoom, new Duration(TimeSpan.FromMilliseconds(_defaultZoomTime)));

            // focus rectangles may need to be repainted.
            _target.InvalidateVisual();
        }

        /// <summary>
        /// Reset any zoom/translate on the target object.
        /// </summary>
        public void Reset()
        {
            StopAnimations();
            _scale.ScaleX = _scale.ScaleY = _value = _newZoom = 1;
            _translate.X = _offset.X = 0;
            _translate.Y = _offset.Y = 0;
            _startTime = 0;
            OnZoomChanged();
            // focus rectangles may need to be repainted.
            _target.InvalidateVisual();
        }

        /// <summary>
        /// Animate the actual zoom by the given amount.  The amount is a measure of how much zoom the
        /// user wants - for example, how hard did they spin the mouse.
        /// </summary>
        /// <param name="clicks">Value between -1 and 1</param>
        private void HandleZoom(Double clicks)
        {
            Double amount = clicks;
            Double oldZoom = _value;
            if (amount > 1) { amount = 1; }
            if (amount < -1) { amount = -1; }

            // If we've changed direction since the last animation then stop animations.
            Boolean sameSign = Math.Sign(amount) == Math.Sign(_lastAmount);
            if (!sameSign || _startTime == 0)
            {
                StopAnimations();
            }

            // Accumulate the desired _newZoom amount as this method is called while the
            // user is repeatedly spinning the mouse.
            Double sensitivity = _sensitivity;
            Double extra = Math.Abs(clicks);
            if (extra > 1)
            {
                // mouse wheel is spinning fast.
                sensitivity = Math.Min(0.5, sensitivity * extra);
            }
            Double delta = 1 - (Math.Abs(amount) * sensitivity);
            if (amount < 0)
            {
                // zoom in
                _newZoom *= delta;
            }
            else
            {
                // zoom out
                _newZoom /= delta;
            }

            // Calculate how long we want to keep zooming (_zoomTime) and increase this time
            // if the user keeps spinning the mouse so we get a nice momentum effect.
            Int64 tick = Environment.TickCount;

            if (sameSign && _startTime != 0 && _startTime + _zoomTime > tick)
            {
                // then make the time cumulative so you get nice smooth animation when you flick the wheel.
                _zoomTime += _startTime + _zoomTime - tick;
                if (_zoomTime > _maxZoomTime) { _zoomTime = _maxZoomTime; }
            }
            else
            {
                _startTime = tick;
                _zoomTime = _defaultZoomTime;
            }
            _lastAmount = amount;

            AnimateZoom(ZoomToPointProperty, oldZoom, _newZoom, new Duration(TimeSpan.FromMilliseconds(_zoomTime)));
        }

        /// <summary>
        /// Start the zoom animation
        /// </summary>
        /// <param name="property">ZoomToPointProperty or ZoomToRectProperty.
        /// This tells us which type of zoom are we doing (around a point or a rectangle)</param>
        /// <param name="oldZoom">The old zoom value</param>
        /// <param name="newZoom">The new value we want to be at</param>
        /// <param name="d">The amound of time we can take to do the animation</param>
        private void AnimateZoom(DependencyProperty property, Double oldZoom, Double newZoom, Duration d)
        {
            MesExponentialDoubleAnimation a = new MesExponentialDoubleAnimation(oldZoom, newZoom, 2, EdgeBehavior.EaseOut, d);
            BeginAnimation(property, a);
        }

        /// <summary>
        /// Scroll the given framework element into view using a smooth animation.
        /// </summary>
        /// <param name="e"></param>
        public Rect ScrollIntoView(FrameworkElement e)
        {
            StopAnimations();
            Rect rect = new Rect(0, 0, e.ActualWidth, e.ActualHeight);
            // Get zoomed & translated coordinates of this object by transforming to the container coordinates.
            rect = e.TransformToAncestor(_container).TransformBounds(rect);
            rect.Inflate(margin, margin);
            ScrollIntoView(rect, new Duration(TimeSpan.FromMilliseconds(_defaultZoomTime)), true);
            return rect;
        }

        /// <summary>
        /// Zoom/scroll all the given framework elements into view
        /// </summary>
        /// <param name="elements">The array of elements</param>
        public void ScrollIntoView(FrameworkElement[] elements)
        {
            StopAnimations();
            Rect result = Rect.Empty;
            foreach (FrameworkElement e in elements)
            {
                Rect rect = new Rect(0, 0, e.ActualWidth, e.ActualHeight);
                // Get zoomed & translated coordinates of this object by transforming to the container coordinates.
                rect = e.TransformToAncestor(_container).TransformBounds(rect);
                if (result == Rect.Empty)
                {
                    result = rect;
                }
                else
                {
                    result.Union(rect);
                }
            }
            result.Inflate(margin, margin);
            ScrollIntoView(result, new Duration(TimeSpan.FromMilliseconds(_defaultZoomTime)), true);
        }

        /// <summary>
        /// Zoom and scroll the given rectangle into view.
        /// </summary>
        /// <param name="rect">The rectangle to scroll into view</param>
        public void ScrollIntoView(Rect rect) => ScrollIntoView(rect, new Duration(TimeSpan.FromMilliseconds(_defaultZoomTime)), true);

        /// <summary>
        /// The margin around the rectangle that we zoom into view so it doesn't take up all the visible space.
        /// </summary>
        private const Double margin = 20;

        /// <summary>
        /// Start the zoom/scroll animation
        /// </summary>
        /// <param name="rect">Rect is in 'container' coordinates.</param>
        /// <param name="duration">Time allowed for the animation</param>
        /// <param name="zoom">Whether we can zoom or not</param>
        private void ScrollIntoView(Rect rect, Duration duration, Boolean zoom)
        {
            Size containerSize = ContainerSize;
            Double width = containerSize.Width;
            Double height = containerSize.Height;

            // Get the bounds of the container so we can see if the selected node is inside these bounds right now.
            Rect window = new Rect(0, 0, width, height);

            if (rect.Width > width || rect.Height > height)
            {
                Rect tr = _container.TransformToDescendant(_target).TransformBounds(rect);
                if (zoom)
                {
                    ZoomToRect(tr);
                    return;
                }
            }

            // Calculate the delta needed to get the node to be visible in the container.
            Point delta = new Point();
            if (rect.Left < window.Left)
            {
                delta.X = window.Left - rect.Left + margin;
            }
            else if (rect.Right > window.Right)
            {
                delta.X = window.Right - rect.Right - margin;
            }
            if (rect.Top < window.Top)
            {
                delta.Y = window.Top - rect.Top + margin;
            }
            else if (rect.Bottom > window.Bottom)
            {
                delta.Y = window.Bottom - rect.Bottom - margin;
            }
            if (delta.X != 0 || delta.Y != 0)
            {
                // Then we do need to move the "target" canvas by changing the current translation so that
                // the selected node is visble.  We need to change the current translation by the 'delta'
                // amount.

                Point startPos = new Point(_translate.X, _translate.Y);
                Double x = startPos.X + delta.X;
                if (x > 0) { x = 0; }
                Double y = startPos.Y + delta.Y;
                if (y > 0) { y = 0; }

                Point newPos = new Point(x, y);
                PointAnimation pa = new PointAnimation(startPos, newPos, duration);
                BeginAnimation(OffsetProperty, pa);
            }
        }

        /// <summary>
        /// Handle the actual animation of the properties
        /// </summary>
        /// <param name="e">Property change information</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ZoomToPointProperty)
            {
                Double o = (Double)e.OldValue;
                Double v = (Double)e.NewValue;
                if (v != 0 && v != o)
                {
                    _value = v;
                    OnZoomChanged();
                    KeepPositionStable();
                }
            }
            else if (e.Property == ZoomToRectProperty)
            {
                Double o = (Double)e.OldValue;
                Double v = (Double)e.NewValue;
                if (v != 0 && v != o)
                {
                    _value = v;
                    OnZoomChanged();
                    KeepRectStable();
                }
            }
            else if (e.Property == OffsetProperty)
            {
                Point p = (Point)e.NewValue;
                Point o = (Point)e.OldValue;
                if (p.X != o.X || p.Y != o.Y)
                {
                    Offset = p;
                }
            }
        }

        /// <summary>
        /// Every Freezable subclass must implement this method.
        /// </summary>
        /// <returns>A new instance of this object</returns>
        protected override Freezable CreateInstanceCore() => new MesMapZoom(_target);
    }
}