using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    /// <summary>
    /// This is used for multi-platform purpose.
    /// This assists in rendering native controll within netstandard.
    /// </summary>
    public interface IMesContentCanvas
    {
        /// <summary>
        /// Sets the background color of the canvas object
        /// </summary>
        /// <param name="color"></param>
        void SetCanvasBackgroundColor(Color color);
    }
}
