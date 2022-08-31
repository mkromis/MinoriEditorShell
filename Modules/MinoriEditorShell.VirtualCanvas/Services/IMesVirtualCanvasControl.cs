using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public class VisualChangeEventArgs : EventArgs
    {
        public int Added { get; set; }
        public int Removed { get; set; }

        public VisualChangeEventArgs(int added, int removed)
        {
            Added = added;
            Removed = removed;
        }
    }

    public interface IMesVirtualCanvasControl
    {
        IMesContentCanvas ContentCanvas { get; }
        double ExtentWidth { get; }
        double ExtentHeight { get; }
        double ViewportWidth { get; }
        double ViewportHeight { get; }

        ObservableCollection<IMesVirtualChild> VirtualChildren { get; }
        SizeF Extent { get; }
        int LiveVisualCount { get; }

        event EventHandler<VisualChangeEventArgs> VisualsChanged;

        void AddVirtualChild(IMesVirtualChild shape);
    }
}