using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MinoriEditorShell.Platforms.Avalonia.Presenters.Attributes;

namespace SimpleDemo.Avalonia.Views
{
    [MesContentPresentation]
    public partial class TipView : UserControl
    {
        public TipView() => InitializeComponent();

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
