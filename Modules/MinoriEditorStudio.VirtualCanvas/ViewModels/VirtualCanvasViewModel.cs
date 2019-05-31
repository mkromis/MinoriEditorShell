//-----------------------------------------------------------------------
// <copyright file="Window1.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorStudio.VirtualCanvas.Gestures;
using MvvmCross.ViewModels;
using System.Windows.Controls;

namespace MinoriEditorStudio.VirtualCanvas.ViewModels
{
    /// <summary>
    /// This demo shows the VirtualCanvas managing up to 50,000 random WPF shapes providing smooth scrolling and
    /// zooming while creating those shapes on the fly.  This helps make a WPF canvas that is a lot more
    /// scalable.
    /// </summary>
    public class VirtualCanvasViewModel : MvxViewModel

    {
        public MapZoom Zoom { get; protected set; }
        public Pan Pan { get; protected set; }
        public RectangleSelectionGesture RectZoom { get; protected set; }
        public AutoScroll AutoScroll { get; protected set; }

        public Controls.VirtualCanvas Graph { get; protected set; }

        public VirtualCanvasViewModel()
        {
            Views.VirtualCanvas canvas = new Views.VirtualCanvas();
            //Graph = canvas.Graph;
            //View = canvas;

            Canvas target = Graph.ContentCanvas;
            Zoom = new MapZoom(target);
            Pan = new Pan(target, Zoom);
            AutoScroll = new AutoScroll(target, Zoom);
            RectZoom = new RectangleSelectionGesture(target, Zoom);
        }
    }
}
