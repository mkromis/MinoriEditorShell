using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public partial class MesSettingsView : Window
    {
        public MesSettingsView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            // Owner = Application.Current.MainWindow;

            // Closing += (s, e) => Owner.Focus();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}