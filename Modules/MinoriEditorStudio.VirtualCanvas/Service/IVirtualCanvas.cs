using MinoriEditorStudio.Services;
using System;

namespace MinoriEditorStudio.VirtualCanvas.Service
{
    public interface IVirtualCanvas : IDocument
    {
        IAutoScroll AutoScroll { get; }
        IVirtualCanvasControl Graph { get; }
        IPan Pan { get; }
        IRectangleSelectionGesture RectZoom { get; }
        IMapZoom Zoom { get; }
        new Boolean CanClose { get; set; }
    }
}