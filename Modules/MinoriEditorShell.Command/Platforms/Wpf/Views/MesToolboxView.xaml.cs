using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MinoriEditorShell.Platforms.Wpf.Controls;
using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.Services;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ToolboxView.xaml
    /// </summary>
    public partial class MesToolboxView : UserControl
    {
        private Boolean _draggingItem;
        private Point _mouseStartPosition;

        public MesToolboxView() => InitializeComponent();

        private void OnListBoxPreviewMouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = MesVisualTreeUtility.FindParent<ListBoxItem>(
                (DependencyObject) e.OriginalSource);
            _draggingItem = listBoxItem != null;

            _mouseStartPosition = e.GetPosition(ListBox);
        }

        private void OnListBoxMouseMove(Object sender, MouseEventArgs e)
        {
            if (!_draggingItem)
            {
                return;
            }

            // Get the current mouse position
            Point mousePosition = e.GetPosition(null);
            Vector diff = _mouseStartPosition - mousePosition;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListBoxItem listBoxItem = MesVisualTreeUtility.FindParent<ListBoxItem>(
                    (DependencyObject) e.OriginalSource);

                if (listBoxItem == null)
                {
                    return;
                }

                MesToolboxItemViewModel itemViewModel = (MesToolboxItemViewModel) ListBox.ItemContainerGenerator.
                    ItemFromContainer(listBoxItem);

                DataObject dragData = new DataObject(MesToolboxDragDrop.DataFormat, itemViewModel.Model);
                DragDrop.DoDragDrop(listBoxItem, dragData, DragDropEffects.Move);
            }
        }
    }
}
