using System;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesMapZoom
    {
        event EventHandler<Double> ValueChanged;
        Double Value { get; set; }
        void ZoomToRect(RectangleF rectangle);
        void ResetTranslate();
    }
}