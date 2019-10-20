using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesVirtualCanvas : IMesDocument
    {
        IMesAutoScroll AutoScroll { get; set; }
        IMesVirtualCanvasControl Graph { get; set; }
        IMesPan Pan { get; set;  }
        IMesRectangleSelectionGesture RectZoom { get; set; }
        IMesMapZoom Zoom { get; set; }
    }
}