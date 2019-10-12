using System.Windows.Controls;
using MinoriEditorShell.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    public partial class StatusBarView : UserControl
    {
        public StatusBarView()
        {
            InitializeComponent();

            // for Design Editor
            try
            {
                IStatusBar statusBar = Mvx.IoCProvider.Resolve<IStatusBar>();
                DataContext = statusBar;
            } catch { }
        }
    }
}
