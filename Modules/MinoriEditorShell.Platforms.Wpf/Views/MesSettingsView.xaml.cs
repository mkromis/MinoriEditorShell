using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using System.Windows;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    [MvxWindowPresentation]
    public partial class MesSettingsView
    {
        /// <summary>
        /// Creates view for settings window.
        /// </summary>
        public MesSettingsView()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;

            Closing += (s, e) => Owner.Focus();
        }
    }
}