using System.Windows.Controls;
using MinoriEditorShell.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    public partial class MesStatusBarView : UserControl
    {
        public MesStatusBarView()
        {
            InitializeComponent();

            // for Design Editor
            try
            {
                IMesStatusBar statusBar = Mvx.IoCProvider.Resolve<IMesStatusBar>();
                DataContext = statusBar;
            } catch { }
        }
    }
}
