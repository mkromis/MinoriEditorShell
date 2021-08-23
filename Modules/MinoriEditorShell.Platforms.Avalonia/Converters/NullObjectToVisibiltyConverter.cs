using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.Windows;

namespace MinoriEditorShell.Platforms.Avalonia.Converters
{
    public class NullObjectToVisibiltyConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) =>
            //(value == null) ? Visibility.Collapsed : Visibility.Visible;
            null;

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => null;
    }
}