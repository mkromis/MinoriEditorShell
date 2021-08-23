using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MinoriEditorShell.Platforms.Avalonia.Presenters.Attributes;
using MinoriEditorShell.Platforms.Avalonia.Views;

namespace SimpleDemo.Avalonia.Views
{
    [MesContentPresentation]
    public partial class TipView : MesAvnView
    {
        public TipView() => InitializeComponent();

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
