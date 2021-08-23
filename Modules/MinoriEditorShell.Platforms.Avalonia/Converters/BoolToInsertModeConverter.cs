using Avalonia.Data.Converters;
using System;

namespace MinoriEditorShell.Platforms.Avalonia.Converters
{
    public class BoolToInsertModeConverter : IValueConverter
    {
        #region IValueConverter Members

        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            Boolean? actValue = value as Boolean?;
            if (actValue == null)
            {
                return null;
            }

            return actValue == false ? "INS" : "OVR";
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter,
                                  System.Globalization.CultureInfo culture) => throw new NotImplementedException();

        #endregion IValueConverter Members
    }
}