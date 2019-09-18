using System;

namespace MinoriEditorStudio.VirtualCanvas.Services
{
    public interface IRectangleSelectionGesture
    {
        event EventHandler ZoomReset;

        ConsoleModifiers ConsoleModifiers { get; set; }
    }
}