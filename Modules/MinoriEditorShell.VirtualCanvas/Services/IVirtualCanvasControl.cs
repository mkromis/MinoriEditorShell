using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IVirtualCanvasControl
    {
        IContentCanvas ContentCanvas { get; }
        IMapZoom Zoom { get; set; }
        Double ExtentWidth { get; }
        Double ExtentHeight { get; }
        Double ViewportWidth { get; }
        Double ViewportHeight { get; }

        ObservableCollection<IVirtualChild> VirtualChildren { get; }

        void AddVirtualChild(IVirtualChild shape);
    }
}