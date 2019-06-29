//-----------------------------------------------------------------------
// <copyright file="RectangleSelectionGesture.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorStudio.VirtualCanvas.Service;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Gestures
{
    /// <summary>
    /// This class provides the ability to draw a rectangle on a zoomable object and zoom into that location.
    /// </summary>
    public class RectangleSelectionGesture : IRectangleSelectionGesture
    {
        private SelectionRectVisual _selectionRectVisual;
        private Point _start;
        private Boolean _watching;
        private readonly FrameworkElement _target;
        private readonly MapZoom _zoom;
        private readonly Panel _container;
        private Point _mouseDownPoint;
        private readonly Int32 _selectionThreshold = 5; // allow some mouse wiggle on mouse down without actually selecting stuff!

        public event EventHandler Selected;
        public event EventHandler ZoomReset;

        /// <summary>
        /// Construct new RectangleSelectionGesture object for selecting things in the given target object.
        /// </summary>
        /// <param name="target">A FrameworkElement</param>
        /// <param name="zoom">The MapZoom object that wraps this same target object</param>
        public RectangleSelectionGesture(IContentCanvas target, IMapZoom zoom)
        {
            _target = (ContentCanvas)target;
            _container = _target.Parent as Panel;
            if (_container == null)
            {
                throw new ArgumentException("Target object must live in a Panel");
            }
            _zoom = (MapZoom)zoom;
            _container.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
            _container.MouseLeftButtonUp += new MouseButtonEventHandler(OnMouseLeftButtonUp);
            _container.MouseMove += new MouseEventHandler(OnMouseMove);
        }

        /// <summary>
        /// Get the rectangle the user drew on the target object.
        /// </summary>
        public Rect SelectionRectangle { get; private set; }

        /// <summary>
        /// Get/Set whether to also zoom the selected rectangle.
        /// </summary>
        public Boolean ZoomSelection { get; set; } = true;

        /// <summary>
        /// Handle the mouse left button down event
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Mouse down information</param>
        void OnMouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled && (Keyboard.Modifiers & ModifierKeys) == ModifierKeys)
            {
                _start = e.GetPosition(_container);
                _watching = true;
                _mouseDownPoint = _start;
            }
        }

        /// <summary>
        /// Get/Set threshold that sets the minimum size rectangle we will allow user to draw.
        /// This allows user to start drawing a rectangle by then change their mind and mouse up
        /// without trigging an almost infinite zoom out to a very smalle piece of real-estate.
        /// </summary>
        public Int32 ZoomSizeThreshold { get; set; } = 20;

        /// <summary>
        /// Specify modifier keys for mouse manipulation
        /// </summary>
        public ModifierKeys ModifierKeys { get; set; } = ModifierKeys.Control;

        /// <summary>
        /// Handle Mouse Move event.  Here we detect whether we've exceeded the _selectionThreshold
        /// and if so capture the mouse and create the visual zoom rectangle on the container object.
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Mouse move information.</param>
        void OnMouseMove(Object sender, MouseEventArgs e)
        {
            if (_watching)
            {
                Point pos = e.GetPosition(_container);
                if (new Vector(_start.X - pos.X, _start.Y - pos.Y).Length > _selectionThreshold)
                {
                    _watching = false;
                    Mouse.Capture(_target, CaptureMode.SubTree);
                    _selectionRectVisual = new SelectionRectVisual(_start, _start, _zoom.Zoom);
                    _container.Children.Add(_selectionRectVisual);
                }
            }
            if (_selectionRectVisual != null)
            {
                if (_selectionRectVisual.Zoom != _zoom.Zoom)
                {
                    _selectionRectVisual.Zoom = _zoom.Zoom;
                }
                _selectionRectVisual.SecondPoint = e.GetPosition(_container);
            }
        }

        /// <summary>
        /// Handle the mouse left button up event.  Here we actually process the selected rectangle
        /// if any by first raising an event for client to receive then also zooming to that rectangle
        /// if ZoomSelection is true
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Mouse button information</param>
        void OnMouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            _watching = false;
            if (_selectionRectVisual != null)
            {
                Mouse.Capture(_target, CaptureMode.None);
                Point pos = e.GetPosition(_container);
                Double f = Math.Min(Math.Abs(pos.X - _mouseDownPoint.X), Math.Abs(pos.Y - _mouseDownPoint.Y));
                Rect r = GetSelectionRect(pos);
                SelectionRectangle = r;

                Selected?.Invoke(this, EventArgs.Empty);

                if (ZoomSelection && f > ZoomSizeThreshold )
                {
                    _zoom.ZoomToRect(r);
                }

                _container.Children.Remove(_selectionRectVisual);
                _selectionRectVisual = null;
            }
            else
            {
                if (e.GetPosition(_container) == _start)
                {
                    ZoomReset?.Invoke(this, EventArgs.Empty);
                }
            }

        }

        /// <summary>
        /// Get the actual selection rectangle that encompasses the mouse down position and the given point.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Rect GetSelectionRect(Point p)
        {
            Rect r = new Rect(_start, p);
            return _container.TransformToDescendant(_target).TransformBounds(r);
        }
    }
}