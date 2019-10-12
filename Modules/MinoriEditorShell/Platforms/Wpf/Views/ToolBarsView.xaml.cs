using MinoriEditorShell.Platforms.Wpf.Services;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ToolBarsView.xaml
    /// </summary>
    public partial class ToolBarsView : UserControl, IToolBarsView
    {
        ToolBarTray IToolBarsView.ToolBarTray => ToolBarTray;

        public ToolBarsView() => InitializeComponent();
    }
}
