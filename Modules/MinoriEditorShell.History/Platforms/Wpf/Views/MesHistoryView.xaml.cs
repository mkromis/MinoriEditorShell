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
    public partial class MesHistoryView : UserControl
    {
        public MesHistoryView() => InitializeComponent();

        private void HistoryItemMouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            MesHistoryViewModel viewModel = (MesHistoryViewModel) DataContext;
            MesHistoryItemViewModel itemViewModel = (MesHistoryItemViewModel) ((FrameworkElement) sender).DataContext;
            viewModel.UndoOrRedoTo(itemViewModel, true);
        }
    }
}
