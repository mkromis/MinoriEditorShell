using System;
using System.Drawing;

namespace MinoriEditorStudio.VirtualCanvas.Service
{
    public interface IVirtualCanvasControl
    {
        IContentCanvas ContentCanvas { get; }
        IMapZoom Zoom { get; set; }
        Double ExtentWidth { get; }
        Double ExtentHeight { get; }
        Double ViewportWidth { get; }
        Double ViewportHeight { get; }
    }
}