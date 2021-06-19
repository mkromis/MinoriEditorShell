using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;

namespace MinoriDemo.RibbonWPF.Views
{
    /// <summary>
    /// Interaction logic for VirtualCanvasView.xaml
    /// </summary>
    [MvxContentPresentation]
    public partial class VirtualCanvasView
    {
        public VirtualCanvasView()
        {
            InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                IMesVirtualCanvas dc = (IMesVirtualCanvas)DataContext;
                Graph.UseDefaultControls(dc);
            };
        }
    }
}