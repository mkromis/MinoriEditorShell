using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.Converters
{
    /// <summary>
    /// Xaml converter for converting drawing color to media color
    /// </summary>
    public class DrawingColorToMediaColorConverter : IValueConverter
    {
        /// <summary>
        /// Convert from Drawing Color to media brush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Drawing.Color? color = value as System.Drawing.Color?;
            return color == null ? null : (object)System.Windows.Media.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
        }
        /// <summary>
        /// Copy back value to drawing color
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color? color = value as Color?;
            return color == null ? System.Drawing.Color.Empty :
                System.Drawing.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
        }
    }
}