using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorShell.VirtualCanvas.Services;
using MinoriEditorShell.VirtualCanvas.ViewModels;
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
                MesVirtualCanvasViewModel dc = (MesVirtualCanvasViewModel)DataContext;
                dc.Graph = Graph;

                IMesContentCanvas canvas = dc.Graph.ContentCanvas;
                dc.Zoom = new MesMapZoom(canvas);
                dc.Pan = new MesPan(canvas, dc.Zoom);
                dc.AutoScroll = new MesAutoScroll(canvas, dc.Zoom);
                dc.RectZoom = new MesRectangleSelectionGesture(canvas, dc.Zoom);
            };
        }
    }
}
