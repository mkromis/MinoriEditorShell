using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MinoriEditorStudio.VirtualCanvas.Service
{
    /// <summary>
    /// This interface is implemented by the objects that you want to put in the VirtualCanvas.
    /// </summary>
    public interface IVirtualChild
    {
        /// <summary>
        /// The bounds of your child object
        /// </summary>
        RectangleF Bounds { get; }

        /// <summary>
        /// Raise this event if the Bounds changes.
        /// </summary>
        event EventHandler BoundsChanged;

        /// <summary>
        /// Return the current Visual or null if it has not been created yet.
        /// </summary>
        Object Visual { get; }

        /// <summary>
        /// Create the WPF visual for this object.
        /// </summary>
        /// <param name="parent">The canvas that is calling this method</param>
        /// <returns>The visual that can be displayed</returns>
        Object CreateVisual(IVirtualCanvasControl parent);

        /// <summary>
        /// Dispose the WPF visual for this object.
        /// </summary>
        void DisposeVisual();
    }
}
