using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.ViewModels;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System.Windows.Controls;

namespace MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for VirtualCanvas
    /// </summary>
    [MvxViewFor(typeof(VirtualCanvasViewModel))]
    public partial class VirtualCanvasView
    {
        public VirtualCanvasView() => InitializeComponent();
    }
}
