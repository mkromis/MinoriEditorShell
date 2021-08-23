using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace MinoriEditorShell.Platforms.Avalonia.Converters
{
    /// <summary>
    /// Convert between media brush and drawing color
    /// </summary>
    public class DrawingColorToMediaBrushConverter : IValueConverter
    {
        /// <summary>
        /// Convert from Drawing Color to meda brush
        /// </summary>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            System.Drawing.Color? color = value as System.Drawing.Color?;
            return color == null ? null :
                new SolidColorBrush(Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B));
        }

        /// <summary>
        /// Reverse convert from media brush to drawing color
        /// </summary>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            Color? color = value as Color?;
            return color == null ? System.Drawing.Color.Empty :
                System.Drawing.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
        }
    }
}