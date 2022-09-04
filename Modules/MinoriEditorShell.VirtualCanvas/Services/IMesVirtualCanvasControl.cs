using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    /// <summary>
    /// Event to handle visual changes
    /// </summary>
    public class VisualChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Specified added child
        /// </summary>
        public int Added { get; internal set; }
        /// <summary>
        /// Specified removed child
        /// </summary>
        public int Removed { get; internal set; }
    }

    /// <summary>
    /// Canvas control interface
    /// </summary>
    public interface IMesVirtualCanvasControl
    {
        /// <summary>
        /// Access to canvas model
        /// </summary>
        IMesContentCanvas ContentCanvas { get; }
        /// <summary>
        /// Total with of content
        /// </summary>
        double ExtentWidth { get; }
        /// <summary>
        /// Content Height
        /// </summary>
        double ExtentHeight { get; }
        /// <summary>
        /// Width of the visual canvas
        /// </summary>
        double ViewportWidth { get; }
        /// <summary>
        /// Height of the visual canvas
        /// </summary>
        double ViewportHeight { get; }
        /// <summary>
        /// Collection of visual children
        /// </summary>
        ObservableCollection<IMesVirtualChild> VirtualChildren { get; }
        /// <summary>
        /// Extents
        /// </summary>
        SizeF Extent { get; }
        /// <summary>
        /// Visible children within viewport
        /// </summary>
        int LiveVisualCount { get; }
        /// <summary>
        /// Event for changed items
        /// </summary>
        event EventHandler<VisualChangeEventArgs> VisualsChanged;
        /// <summary>
        /// Helper to add children
        /// </summary>
        /// <param name="child">Child to add</param>
        void AddVirtualChild(IMesVirtualChild child);
    }
}