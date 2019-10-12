using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    public class LightTheme : ITheme
    {
        public virtual String Name => Properties.Resources.ThemeLightName;

        public virtual IEnumerable<Uri> ApplicationResources
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

                yield return new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/LightTheme.xaml");
                //yield return new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/LightTheme.xaml");
                //yield return new Uri("pack://application:,,,/MinoriEditorStudio;component/Themes/VS2013/LightTheme.xaml");
            }
        }
    }
}
