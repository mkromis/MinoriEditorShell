using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesVirtualCanvasControl
    {
        IMesContentCanvas ContentCanvas { get; }
        IMesMapZoom Zoom { get; set; }
        Double ExtentWidth { get; }
        Double ExtentHeight { get; }
        Double ViewportWidth { get; }
        Double ViewportHeight { get; }

        ObservableCollection<IMesVirtualChild> VirtualChildren { get; }

        void AddVirtualChild(IMesVirtualChild shape);
    }
}