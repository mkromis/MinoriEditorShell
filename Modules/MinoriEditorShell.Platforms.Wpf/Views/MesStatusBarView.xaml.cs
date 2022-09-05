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
        /// <summary>
        /// Creates status bar view with IMesStatusBar backing
        /// </summary>
        public MesStatusBarView()
        {
            InitializeComponent();

            // for Design Editor
            DataContext = Mvx.IoCProvider.Resolve<IMesStatusBar>();
        }
    }
}