//-----------------------------------------------------------------------
// <copyright file="Pan.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using MinoriEditorStudio.VirtualCanvas.Service;
using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls;

namespace MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Gestures
{

    /// <summary>
    /// This class provides the ability to pan the target object when dragging the mouse 
    /// </summary>
    public class Pan : IPan
    {

        private Boolean _dragging;
        private readonly FrameworkElement _target;
        private readonly MapZoom _zoom;
        private Boolean _captured;
        private readonly Panel _container;
        private Point _mouseDownPoint;
        private Point _startTranslate;
        private readonly ModifierKeys _mods = ModifierKeys.None;

        /// <summary>
        /// Construct new Pan gesture object.
        /// </summary>
        /// <param name="target">The target to be panned, must live inside a container Panel</param>
        /// <param name="zoom"></param>
        public Pan(IContentCanvas target, IMapZoom zoom) {
            _target = (ContentCanvas)target;
            _container = _target.Parent as Panel;
            if (_container == null) {
                // todo: localization
                throw new ArgumentException("Target object must live in a Panel");
            }
            _zoom = (MapZoom)zoom;
            _container.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
            _container.MouseLeftButtonUp += new MouseButtonEventHandler(OnMouseLeftButtonUp);
            _container.MouseMove += new MouseEventHandler(OnMouseMove);
        }

        /// <summary>
        /// Handle mouse left button event on container by recording that position and setting
        /// a flag that we've received mouse left down.
        /// </summary>
        /// <param name="sender">Container</param>
        /// <param name="e">Mouse information</param>
        void OnMouseLeftButtonDown(Object sender, MouseButtonEventArgs e) {

            ModifierKeys mask = Keyboard.Modifiers & _mods;
            if (!e.Handled && mask == _mods && mask == Keyboard.Modifiers)
            {
                _container.Cursor = Cursors.Hand;
                _mouseDownPoint = e.GetPosition(_container);
                Point offset = _zoom.Offset;
                _startTranslate = new Point(offset.X, offset.Y);
                _dragging = true;
            }
        }

        /// <summary>
        /// Handle the mouse move event and this is where we capture the mouse.  We don't want
        /// to actually start panning on mouse down.  We want to be sure the user starts dragging
        /// first.
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Move information</param>
        void OnMouseMove(Object sender, MouseEventArgs e) {
            if (_dragging) {
                if (!_captured) {
                    _captured = true;
                    _target.Cursor = Cursors.Hand;
                    Mouse.Capture(_target, CaptureMode.SubTree);
                }
                MoveBy(_mouseDownPoint - e.GetPosition(_container));
            }
        }

        /// <summary>
        /// Handle the mouse left button up event and stop any panning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseLeftButtonUp(Object sender, MouseButtonEventArgs e) {

            if (_captured) {
                Mouse.Capture(_target, CaptureMode.None);
                e.Handled = true;
                _target.Cursor = Cursors.Arrow; ;
                _captured = false;   
            }

            _dragging = false;
        }

        /// <summary>
        /// Move the target object by the given delta delative to the start scroll position we recorded in mouse down event.
        /// </summary>
        /// <param name="v">A vector containing the delta from recorded mouse down position and current mouse position</param>
        public void MoveBy(Vector v) {
            _zoom.Offset = new Point(_startTranslate.X - v.X, _startTranslate.Y - v.Y);
            _target.InvalidateVisual();
        }


    }
}
