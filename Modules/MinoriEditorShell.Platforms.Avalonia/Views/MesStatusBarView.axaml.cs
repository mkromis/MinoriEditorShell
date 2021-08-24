using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MinoriEditorShell.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public partial class MesStatusBarView : UserControl
    {
        public MesStatusBarView()
        {
            InitializeComponent();

            // for Design Editor
            try
            {
                DataContext = Mvx.IoCProvider.Resolve<IMesStatusBar>();
            }
            catch { }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}