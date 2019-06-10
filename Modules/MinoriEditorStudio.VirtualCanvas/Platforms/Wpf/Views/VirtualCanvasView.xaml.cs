using MinoriEditorStudio.VirtualCanvas.ViewModels;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System.Windows.Controls;

namespace MinoriEditorStudio.VirtualCanvas.Views
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
