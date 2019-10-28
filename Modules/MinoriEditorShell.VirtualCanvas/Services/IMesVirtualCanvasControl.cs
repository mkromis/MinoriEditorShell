using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesVirtualCanvasControl
    {
        IMesContentCanvas ContentCanvas { get; }
        Double ExtentWidth { get; }
        Double ExtentHeight { get; }
        Double ViewportWidth { get; }
        Double ViewportHeight { get; }

        ObservableCollection<IMesVirtualChild> VirtualChildren { get; }
        SizeF Extent { get; }

        void AddVirtualChild(IMesVirtualChild shape);
    }
}