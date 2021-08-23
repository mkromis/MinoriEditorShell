using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.Windows;

namespace MinoriEditorShell.Platforms.Avalonia.Converters
{
    /// <summary>
    /// Converts from nullss
    /// </summary>
    public class NullableValueConverter : IValueConverter
    {
        /// <summary>
        ///
        /// </summary>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) =>
            value ?? AvaloniaProperty.UnsetValue;

        /// <summary>
        /// Do nothing
        /// </summary>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}