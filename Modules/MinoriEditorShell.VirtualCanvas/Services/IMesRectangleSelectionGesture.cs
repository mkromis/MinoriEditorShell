using System;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesRectangleSelectionGesture
    {
        event EventHandler ZoomReset;

        ConsoleModifiers ConsoleModifiers { get; set; }
    }
}