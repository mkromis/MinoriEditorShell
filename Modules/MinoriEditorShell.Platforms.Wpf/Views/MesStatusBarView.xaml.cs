using MinoriEditorShell.Services;
using MvvmCross;
using System.Windows.Controls;

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
            }
            catch { }
        }
    }
}