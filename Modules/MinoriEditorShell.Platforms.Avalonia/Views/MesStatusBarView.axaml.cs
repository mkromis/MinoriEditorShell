using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public partial class MesStatusBarView : UserControl
    {
        public MesStatusBarView()
        {
            InitializeComponent();

            //         // for Design Editor
            //         try
            //         {
            //             IMesStatusBar statusBar = Mvx.IoCProvider.Resolve<IMesStatusBar>();
            //             DataContext = statusBar;
            //         }
            //         catch { }
            //     }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}