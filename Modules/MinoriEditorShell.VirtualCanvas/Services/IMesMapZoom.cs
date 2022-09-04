using System;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    /// <summary>
    /// Notify when values changed
    /// </summary>
    public class MesMapZoomEventArgs : EventArgs 
    {
        /// <summary>
        /// The current value
        /// </summary>
        public double Value { get; set; }
    }


    /// <summary>
    /// Map Zoom interactions
    /// </summary>
    public interface IMesMapZoom
    {
        /// <summary>
        /// Notify when values changed
        /// </summary>
        event EventHandler<MesMapZoomEventArgs> ValueChanged;

        /// <summary>
        /// Current Value
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Constructor for the rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        void ZoomToRect(RectangleF rectangle);

        /// <summary>
        /// Reset the value
        /// </summary>
        void ResetTranslate();
    }
}