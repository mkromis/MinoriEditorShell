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
        public MesSettingsView()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;

            Closing += (s, e) => Owner.Focus();
        }
    }
}