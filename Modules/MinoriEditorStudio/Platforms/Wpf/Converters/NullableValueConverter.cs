using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MinoriEditorStudio.Platforms.Wpf.Converters
{
    public class NullableValueConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, object parameter, CultureInfo culture) => value == null ? DependencyProperty.UnsetValue : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
