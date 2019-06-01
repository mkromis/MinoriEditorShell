using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MinoriEditorStudio.Modules.StatusBar.ViewModels;
using MvvmCross;

namespace MinoriEditorStudio.Modules.StatusBar.Views
{
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    public partial class StatusBarView : UserControl
    {
        private Grid _statusBarGrid;

        public StatusBarView()
        {
            InitializeComponent();
            IStatusBar statusBar = Mvx.IoCProvider.Resolve<IStatusBar>();
            statusBar.Items.CollectionChanged += (s, e) => RefreshGridColumns();
            DataContext = statusBar;
        }

        private void OnStatusBarGridLoaded(Object sender, RoutedEventArgs e)
        {
            _statusBarGrid = (Grid) sender;
            RefreshGridColumns();
        }

        private void RefreshGridColumns()
        {
            _statusBarGrid.ColumnDefinitions.Clear();
            foreach (StatusBarItemViewModel item in StatusBar.Items.Cast<StatusBarItemViewModel>())
            {
                _statusBarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = item.Width });
            }
        }
    }
}
