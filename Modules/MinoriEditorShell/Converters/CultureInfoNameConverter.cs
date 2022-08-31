using MvvmCross.Converters;
using System;
using System.Globalization;

namespace MinoriEditorShell.Converters
{
    /// <summary>
    /// Language converter from resources
    /// </summary>
    public class CultureInfoNameConverter : MvxValueConverter<string, string>
    {
        /// <summary>
        /// Convert from resource file to xaml bindings
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Properties.Resources.LanguageSystem.Equals("System", StringComparison.Ordinal) 
                    ? Properties.Resources.LanguageSystem 
                    : $"{Properties.Resources.LanguageSystem} ({Properties.Resources.ResourceManager.GetString("LanguageSystem", CultureInfo.InvariantCulture)})";
            }

            CultureInfo ci = CultureInfo.GetCultureInfo(value);

            return Equals(ci.NativeName, ci.EnglishName) 
                ? ci.NativeName 
                : $"{ci.NativeName} ({ci.EnglishName})";
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected override string ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}