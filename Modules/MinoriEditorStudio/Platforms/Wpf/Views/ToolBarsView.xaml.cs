using MinoriEditorStudio.Platforms.Wpf.Services;
using System.Windows.Controls;

namespace MinoriEditorStudio.Modules.ToolBars.Views
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
