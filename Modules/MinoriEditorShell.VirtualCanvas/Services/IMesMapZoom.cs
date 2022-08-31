using System;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesMapZoom
    {
        event EventHandler<double> ValueChanged;

        double Value { get; set; }

        void ZoomToRect(RectangleF rectangle);

        void ResetTranslate();
    }
}