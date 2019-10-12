using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IVirtualCanvas : IDocument
    {
        IAutoScroll AutoScroll { get; }
        IVirtualCanvasControl Graph { get; }
        IPan Pan { get; }
        IRectangleSelectionGesture RectZoom { get; }
        IMapZoom Zoom { get; }
    }
}