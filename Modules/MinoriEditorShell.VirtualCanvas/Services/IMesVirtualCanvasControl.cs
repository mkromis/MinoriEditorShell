using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public class VisualChangeEventArgs : EventArgs
    {
        public Int32 Added { get; set; }
        public Int32 Removed { get; set; }
        public VisualChangeEventArgs(Int32 added, Int32 removed)
        {
            Added = added;
            Removed = removed;
        }
    }

    public interface IMesVirtualCanvasControl
    {
        IMesContentCanvas ContentCanvas { get; }
        Double ExtentWidth { get; }
        Double ExtentHeight { get; }
        Double ViewportWidth { get; }
        Double ViewportHeight { get; }

        ObservableCollection<IMesVirtualChild> VirtualChildren { get; }
        SizeF Extent { get; }
        Int32 LiveVisualCount { get; }

        event EventHandler<VisualChangeEventArgs> VisualsChanged;

        void AddVirtualChild(IMesVirtualChild shape);
    }
}