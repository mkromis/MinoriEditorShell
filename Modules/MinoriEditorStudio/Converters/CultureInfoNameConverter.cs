using MvvmCross.Converters;
using System;
using System.Globalization;

namespace MinoriEditorStudio.Converters
{
    public class CultureInfoNameConverter : IMvxValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (String.Empty.Equals(value))
            {
                if (Properties.Resources.LanguageSystem.Equals("System"))
                {
                    return Properties.Resources.LanguageSystem;
                }

                return string.Format("{0} ({1})",
                    Properties.Resources.LanguageSystem,
                    Properties.Resources.ResourceManager.GetString("LanguageSystem", CultureInfo.InvariantCulture)
                    );
            }

            String cn = value as String;
            CultureInfo ci = CultureInfo.GetCultureInfo(cn);

            if (Equals(ci.NativeName, ci.EnglishName))
                return ci.NativeName;

            return String.Format("{0} ({1})", ci.NativeName, ci.EnglishName);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
