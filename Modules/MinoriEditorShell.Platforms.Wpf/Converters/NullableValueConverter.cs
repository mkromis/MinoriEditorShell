using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MinoriEditorShell.Platforms.Wpf.Converters
{
    /// <summary>
    /// Nullable to value for xaml
    /// </summary>
    public class NullableValueConverter : IValueConverter
    {
        /// <summary>
        /// Return value, if value is null return unset value
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value ?? DependencyProperty.UnsetValue;

        /// <summary>
        /// No reverse conversion
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            throw new NotSupportedException();
    }
}