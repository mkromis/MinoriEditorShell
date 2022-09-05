using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.Converters
{
    /// <summary>
    /// Converter used in xaml for UI conversion
    /// </summary>
    public class DrawingColorToMediaBrushConverter : IValueConverter
    {
        /// <summary>
        /// Convert from Drawing Color to media brush
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Drawing.Color? color = value as System.Drawing.Color?;
            return color == null ? null :
                new SolidColorBrush(Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B));
        }

        /// <summary>
        /// Convert media brush to model's Drawing Color
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color? color = value as Color?;
            return color == null ? System.Drawing.Color.Empty :
                System.Drawing.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
        }
    }
}