using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.Windows;

namespace MinoriEditorShell.Platforms.Avalonia.Converters
{
    public class TreeViewIndentConverter : IValueConverter
    {
        public double Indent { get; set; }

        private static int GetItemDepth(TreeViewItem item)
        {
            AvaloniaObject target = item;

            var depth = 0;
            //do
            //{
            //    if (target is TreeView)
            //        return depth - 1;
            //    if (target is TreeViewItem)
            //        depth++;
            //} while ((target = VisualTreeHelper.GetParent(target)) != null);

            //return 0;
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TreeViewItem item))
                return new Thickness(0);

            return new Thickness(Indent * GetItemDepth(item), 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}