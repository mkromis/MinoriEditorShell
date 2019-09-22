using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MinoriEditorStudio.Platforms.Wpf.Converters
{
    public class DrawingColorToMediaBrushConverter : IValueConverter
    {
        // Convert from Drawing Color to meda brush
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            System.Drawing.Color? color = value as System.Drawing.Color?;
            return color == null ? null : 
                new SolidColorBrush(Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B));
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            Color? color = value as Color?;
            return color == null ? System.Drawing.Color.Empty :
                System.Drawing.Color.FromArgb(color.Value.R, color.Value.G, color.Value.B);
        }
    }
}
