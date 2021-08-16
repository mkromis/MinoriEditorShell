using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MinoriEditorShell.Platforms.Avalonia.Views;

namespace SimpleDemo.Avalonia.Views
{
    public partial class MainView : MesAvnView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
