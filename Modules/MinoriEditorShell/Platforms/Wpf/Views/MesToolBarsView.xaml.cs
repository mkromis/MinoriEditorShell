using MinoriEditorShell.Platforms.Wpf.Services;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ToolBarsView.xaml
    /// </summary>
    public partial class MesToolBarsView : UserControl, IToolBarsView
    {
        ToolBarTray IToolBarsView.ToolBarTray => ToolBarTray;

        public MesToolBarsView() => InitializeComponent();
    }
}
