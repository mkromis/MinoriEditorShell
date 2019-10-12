using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public HistoryView() => InitializeComponent();

        private void HistoryItemMouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            HistoryViewModel viewModel = (HistoryViewModel) DataContext;
            HistoryItemViewModel itemViewModel = (HistoryItemViewModel) ((FrameworkElement) sender).DataContext;
            viewModel.UndoOrRedoTo(itemViewModel, true);
        }
    }
}
