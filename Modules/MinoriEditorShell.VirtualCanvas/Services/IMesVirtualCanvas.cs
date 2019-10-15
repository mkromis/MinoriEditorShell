using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesVirtualCanvas : IDocument
    {
        IMesAutoScroll AutoScroll { get; }
        IMesVirtualCanvasControl Graph { get; }
        IMesPan Pan { get; }
        IMesRectangleSelectionGesture RectZoom { get; }
        IMesMapZoom Zoom { get; }
    }
}