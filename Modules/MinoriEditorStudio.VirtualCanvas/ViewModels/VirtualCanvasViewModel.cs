﻿using MinoriEditorStudio.Services;
using MinoriEditorStudio.VirtualCanvas.Gestures;
using MinoriEditorStudio.VirtualCanvas.Service;
using MinoriEditorStudio.VirtualCanvas.Views;
using System;
using System.Windows.Controls;

namespace MinoriEditorStudio.VirtualCanvas.ViewModels
{
    /// <summary>
    /// This demo shows the VirtualCanvas managing up to 50,000 random WPF shapes providing smooth scrolling and
    /// zooming while creating those shapes on the fly.  This helps make a WPF canvas that is a lot more
    /// scalable.
    /// </summary>
    public class VirtualCanvasViewModel : Document, IVirtualCanvas
    {
        public MapZoom Zoom { get; protected set; }
        public Pan Pan { get; protected set; }
        public RectangleSelectionGesture RectZoom { get; protected set; }
        public AutoScroll AutoScroll { get; protected set; }
        public Controls.VirtualCanvas Graph { get; protected set; }
        public new Boolean CanClose { get; set; }

        public override void ViewAppeared()
        {
            // This is being called twice or not at all.
            if (Graph == null)
            {
                Graph = ((VirtualCanvasView)View).Graph;
                Canvas target = Graph.ContentCanvas;
                Graph.Zoom = Zoom = new MapZoom(target);
                Pan = new Pan(target, Zoom);
                AutoScroll = new AutoScroll(target, Zoom);
                RectZoom = new RectangleSelectionGesture(target, Zoom);
            }
        }
    }
}
