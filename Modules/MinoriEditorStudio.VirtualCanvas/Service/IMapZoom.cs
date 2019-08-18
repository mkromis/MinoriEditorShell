using System;
using System.Drawing;

namespace MinoriEditorStudio.VirtualCanvas.Service
{
    public interface IMapZoom
    {
        event EventHandler<Double> ValueChanged;
        Double Value { get; set; }
    }
}