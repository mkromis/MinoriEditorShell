using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MinoriEditorShell.Platforms.Avalonia.Presenters.Attributes;
using MinoriEditorShell.Platforms.Avalonia.Views;

namespace SimpleDemo.Avalonia
{
    [MesWindowPresentation]
    public class MainWindow : MesWindow
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
