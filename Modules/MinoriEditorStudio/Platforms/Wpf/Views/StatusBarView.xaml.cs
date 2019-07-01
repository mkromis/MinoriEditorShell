using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MinoriEditorStudio.Modules.StatusBar.ViewModels;
using MinoriEditorStudio.Services;
using MvvmCross;

namespace MinoriEditorStudio.Platforms.Wpf.Views
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
