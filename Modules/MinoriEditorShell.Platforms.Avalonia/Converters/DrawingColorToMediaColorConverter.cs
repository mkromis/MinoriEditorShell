﻿using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace MinoriEditorShell.Platforms.Avalonia.Converters
{
    /// <summary>
    /// Drawing color to media color
    /// </summary>
    public class DrawingColorToMediaColorConverter : IValueConverter
    {
        /// <summary>
        /// Convert from Drawing Color to meda brush
        /// </summary>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            System.Drawing.Color? color = value as System.Drawing.Color?;
            return color == null ? null : (Object)Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
        }

        /// <summary>
        /// Back convert to drawing color from media brush
        /// </summary>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            Color? color = value as Color?;
            return color == null ? System.Drawing.Color.Empty :
                System.Drawing.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
        }
    }
}