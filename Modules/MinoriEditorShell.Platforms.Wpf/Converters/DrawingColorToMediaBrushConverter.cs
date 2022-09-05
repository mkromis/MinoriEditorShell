using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.Converters
{
#warning Not Implemented
#if false
    public class DrawingColorToMediaBrushConverter : IValueConverter
    {
        // Convert from Drawing Color to meda brush
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Drawing.Color? color = value as System.Drawing.Color?;
            return color == null ? null :
                new SolidColorBrush(Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color? color = value as Color?;
            return color == null ? System.Drawing.Color.Empty :
                System.Drawing.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
        }
    }
#endif
}