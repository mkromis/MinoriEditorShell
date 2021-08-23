using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public partial class MesGeneralSettingsView : UserControl
    {
        public MesGeneralSettingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
