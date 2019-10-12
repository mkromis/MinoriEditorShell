using System;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IRectangleSelectionGesture
    {
        event EventHandler ZoomReset;

        ConsoleModifiers ConsoleModifiers { get; set; }
    }
}