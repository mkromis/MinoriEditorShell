using MinoriEditorStudio.Framework;
using MinoriEditorStudio.VirtualCanvas.Controls;
using MinoriEditorStudio.VirtualCanvas.Gestures;
using System;

namespace MinoriEditorStudio.VirtualCanvas.Service
{
    public interface IVirtualCanvas : IDocument
    {
        AutoScroll AutoScroll { get; }
        Controls.VirtualCanvas Graph { get; }
        Pan Pan { get; }
        RectangleSelectionGesture RectZoom { get; }
        MapZoom Zoom { get; }
        new Boolean CanClose { get; set; }

        void EnsureLoaded();
    }
}