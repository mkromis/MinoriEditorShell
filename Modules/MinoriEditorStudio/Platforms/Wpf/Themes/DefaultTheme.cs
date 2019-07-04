using MinoriEditorStudio.Platforms.Wpf.Extensions;
using MinoriEditorStudio.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorStudio.Platforms.Wpf.Themes
{
    /// <summary>
    /// Class DefaultTheme
    /// </summary>
    public sealed class DefaultTheme : ITheme
    {
        /// <summary>
        /// The name of the theme - "Default"
        /// </summary>
        /// <value>The name.</value>
        public String Name => "Default";

        /// <summary>
        /// Lists of valid URIs which will be loaded in the theme dictionary
        /// </summary>
        /// <value>The URI list.</value>
        public IEnumerable<Uri> ApplicationResources
        {
            get
            {
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml");
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml");
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml");
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml");

                if (this.HasRibbon())
                {
                    yield return new Uri("pack://application:,,,/Fluent;Component/Themes/Generic.xaml");
                    yield return new Uri("pack://application:,,,/Fluent;component/Themes/Accents/Cobalt.xaml");
                    yield return new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseLight.xaml");
                }

                yield return new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.Aero;component/Theme.xaml");
            }
        }
    }
}