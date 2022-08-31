//-----------------------------------------------------------------------
// <copyright file="RectangleSelectionGesture.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures
{
    /// <summary>
    /// This class provides the ability to draw a rectangle on a zoomable object and zoom into that location.
    /// </summary>
    public class MesRectangleSelectionGesture : IMesRectangleSelectionGesture
    {
        private MesSelectionRectVisual _selectionRectVisual;
        private Point _start;
        private bool _watching;
        private readonly FrameworkElement _target;
        private readonly MesMapZoom _zoom;
        private readonly Panel _container;
        private Point _mouseDownPoint;
        private readonly int _selectionThreshold = 5; // allow some mouse wiggle on mouse down without actually selecting stuff!

        public event EventHandler Selected;

        public event EventHandler ZoomReset;

        /// <summary>
        /// Construct new RectangleSelectionGesture object for selecting things in the given target object.
        /// </summary>
        /// <param name="target">A FrameworkElement</param>
        /// <param name="zoom">The MapZoom object that wraps this same target object</param>
        public MesRectangleSelectionGesture(IMesContentCanvas target, IMesMapZoom zoom)
        {
            _target = (MesContentCanvas)target;
            _container = _target.Parent as Panel;
            if (_container == null)
            {
                throw new ArgumentException("Target object must live in a Panel");
            }
            _zoom = (MesMapZoom)zoom;
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
        public bool ZoomSelection { get; set; } = true;

        /// <summary>
        /// Handle the mouse left button down event
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Mouse down information</param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ModifierKeys modifier = ConsoleModifiers.ToModifierKeys();
            if (!e.Handled && (Keyboard.Modifiers & modifier) == modifier)
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
        public int ZoomSizeThreshold { get; set; } = 20;

        /// <summary>
        /// Specify modifier keys for mouse manipulation
        /// </summary>
        public ConsoleModifiers ConsoleModifiers { get; set; } = ConsoleModifiers.Control;

        /// <summary>
        /// Handle Mouse Move event.  Here we detect whether we've exceeded the _selectionThreshold
        /// and if so capture the mouse and create the visual zoom rectangle on the container object.
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Mouse move information.</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_watching)
            {
                Point pos = e.GetPosition(_container);
                if (new Vector(_start.X - pos.X, _start.Y - pos.Y).Length > _selectionThreshold)
                {
                    _watching = false;
                    Mouse.Capture(_target, CaptureMode.SubTree);
                    _selectionRectVisual = new MesSelectionRectVisual(_start, _start, _zoom.Value);
                    _container.Children.Add(_selectionRectVisual);
                }
            }
            if (_selectionRectVisual != null)
            {
                if (_selectionRectVisual.Zoom != _zoom.Value)
                {
                    _selectionRectVisual.Zoom = _zoom.Value;
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
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _watching = false;
            if (_selectionRectVisual != null)
            {
                Mouse.Capture(_target, CaptureMode.None);
                Point pos = e.GetPosition(_container);
                double f = Math.Min(Math.Abs(pos.X - _mouseDownPoint.X), Math.Abs(pos.Y - _mouseDownPoint.Y));
                Rect r = GetSelectionRect(pos);
                SelectionRectangle = r;

                Selected?.Invoke(this, EventArgs.Empty);

                if (ZoomSelection && f > ZoomSizeThreshold)
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
        private Rect GetSelectionRect(Point p)
        {
            Rect r = new Rect(_start, p);
            return _container.TransformToDescendant(_target).TransformBounds(r);
        }
    }
}