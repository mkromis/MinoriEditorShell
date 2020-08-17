using MvvmCross.Converters;
using System;
using System.Globalization;

namespace MinoriEditorShell.Converters
{
    /// <summary>
    /// Language converter from resources
    /// </summary>
    public class CultureInfoNameConverter : MvxValueConverter<String, String>
    {
        /// <summary>
        /// Convert from resource file to xaml bindings
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected override String Convert(String value, Type targetType, Object parameter, CultureInfo culture)
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

                return String.Format("{0} ({1})",
                    Properties.Resources.LanguageSystem,
                    Properties.Resources.ResourceManager.GetString("LanguageSystem", CultureInfo.InvariantCulture)
                    );
            }

            CultureInfo ci = CultureInfo.GetCultureInfo(value);

            if (Equals(ci.NativeName, ci.EnglishName))
            {
                return ci.NativeName;
            }

            return String.Format("{0} ({1})", ci.NativeName, ci.EnglishName);
        }

        /// <summary>
        /// Not impemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected override String ConvertBack(String value, Type targetType, Object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}