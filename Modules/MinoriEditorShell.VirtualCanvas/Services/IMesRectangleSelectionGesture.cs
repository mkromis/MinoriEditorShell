using System;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    /// <summary>
    /// This class provides the ability to draw a rectangle on a zoom-able object and zoom into that location.
    /// </summary>
    public interface IMesRectangleSelectionGesture
    {
        /// <summary>
        /// Event for when needed to reset view
        /// </summary>
        event EventHandler ZoomReset;
        /// <summary>
        /// Event for when needed to selection changed
        /// </summary>
        event EventHandler Selected;

        /// <summary>
        /// Keys pressed during rectangle gesture
        /// </summary>
        ConsoleModifiers ConsoleModifiers { get; set; }
    }
}