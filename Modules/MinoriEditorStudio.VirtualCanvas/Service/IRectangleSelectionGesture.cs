using System;

namespace MinoriEditorStudio.VirtualCanvas.Service
{
    public interface IRectangleSelectionGesture
    {
        event EventHandler ZoomReset;

        ConsoleModifiers ConsoleModifiers { get; set; }
    }
}