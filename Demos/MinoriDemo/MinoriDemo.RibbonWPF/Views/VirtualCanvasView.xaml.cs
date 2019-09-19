﻿using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorStudio.VirtualCanvas.Services;
using MinoriEditorStudio.VirtualCanvas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinoriDemo.RibbonWPF.Views
{
    /// <summary>
    /// Interaction logic for VirtualCanvasView.xaml
    /// </summary>
    public partial class VirtualCanvasView
    {
        public VirtualCanvasView()
        {
            InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                VirtualCanvasViewModel dc = (VirtualCanvasViewModel)DataContext;
                dc.Graph = Graph;

                IContentCanvas canvas = dc.Graph.ContentCanvas;
                dc.Zoom = new MapZoom(canvas);
                dc.Pan = new Pan(canvas, dc.Zoom);
                dc.AutoScroll = new AutoScroll(canvas, dc.Zoom);
                dc.RectZoom = new RectangleSelectionGesture(canvas, dc.Zoom);
            };
        }
    }
}
