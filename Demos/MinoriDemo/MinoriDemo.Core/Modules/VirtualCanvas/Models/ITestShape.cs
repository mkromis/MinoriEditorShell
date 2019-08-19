using MinoriEditorStudio.VirtualCanvas.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MinoriDemo.Core.Modules.VirtualCanvas.Models
{
    public interface ITestShape : IVirtualChild
    {
        Color Fill { get; set; }
        Color Stroke { get; set; }
        String Label { get; set; }

        void Initialize(RectangleF bounds, TestShapeType shape, Random r);
    }
}
