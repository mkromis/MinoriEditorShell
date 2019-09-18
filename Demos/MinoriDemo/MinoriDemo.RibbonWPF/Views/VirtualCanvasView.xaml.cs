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

#warning FIX INIT
            //VirtualCanvasViewModel viewModel = ((IVirtualCanvasViewModel)DataContext).Graph;
            //Graph = ((VirtualCanvasView)View).Graph;
            //IContentCanvas target = Graph.ContentCanvas;
            //Graph.Zoom = Zoom = new MapZoom(target);
            //Pan = new Pan(target, Zoom);
            //AutoScroll = new AutoScroll(target, Zoom);
            //RectZoom = new RectangleSelectionGesture(target, Zoom);
        }
    }
}
