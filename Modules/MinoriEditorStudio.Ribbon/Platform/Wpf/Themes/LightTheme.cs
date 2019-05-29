using MinoriEditorStudio.Modules.Themes.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorStudio.Ribbon.Platform.Wpf.Controls
{
    /// <summary>
    /// Class DefaultTheme
    /// </summary>
    public sealed class LightTheme : ITheme
    {
        /// <summary>
        /// The name of the theme - "Default"
        /// </summary>
        /// <value>The name.</value>
        public String Name => "Light";

        /// <summary>
        /// Lists of valid URIs which will be loaded in the theme dictionary
        /// </summary>
        /// <value>The URI list.</value>
        public IEnumerable<Uri> ApplicationResources { get; } = new List<Uri>
        {
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml"),
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml"),
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml"),
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml"),

            new Uri("pack://application:,,,/Fluent;Component/Themes/Generic.xaml"),
            new Uri("pack://application:,,,/Fluent;component/Themes/Accents/Cobalt.xaml"),
            new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseLight.xaml"),

            new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/LightTheme.xaml"),
        };

        public IEnumerable<Uri> MainWindowResources { get; }
    }
}