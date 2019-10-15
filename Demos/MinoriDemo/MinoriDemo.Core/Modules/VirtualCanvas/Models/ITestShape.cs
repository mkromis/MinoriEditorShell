﻿using MinoriEditorShell.VirtualCanvas.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MinoriDemo.Core.Modules.VirtualCanvas.Models
{
    public interface ITestShape : IMesVirtualChild
    {
        Color BaseColor { get; set; }
        String Label { get; set; }

        void Initialize(RectangleF bounds, TestShapeType shape, Random r);
    }
}
