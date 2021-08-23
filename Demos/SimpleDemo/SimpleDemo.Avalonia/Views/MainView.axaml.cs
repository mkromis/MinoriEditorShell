using Avalonia.Markup.Xaml;
using MinoriEditorShell.Platforms.Avalonia.Presenters.Attributes;
using MinoriEditorShell.Platforms.Avalonia.Views;

namespace SimpleDemo.Avalonia.Views
{
    [MesContentPresentation]
    public partial class MainView : MesAvnView
    {
        public MainView() => InitializeComponent();

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
