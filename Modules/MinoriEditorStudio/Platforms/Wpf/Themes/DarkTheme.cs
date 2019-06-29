using MinoriEditorStudio.Platforms.Wpf.Extensions;
using MinoriEditorStudio.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorStudio.Platforms.Wpf.Themes
{
    public class DarkTheme : ITheme
    {
        public virtual String Name => Properties.Resources.ThemeDarkName;

        public virtual IEnumerable<Uri> ApplicationResources
        {
            get
            {
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml");
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml");
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml");
                yield return new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml");

                if (this.HasRibbon()) {
                    yield return new Uri("pack://application:,,,/Fluent;Component/Themes/Generic.xaml");
                    yield return new Uri("pack://application:,,,/Fluent;component/Themes/Accents/Cobalt.xaml");
                    yield return new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseDark.xaml");
                }

                //new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/DarkTheme.xaml"),

                yield return new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/DarkTheme.xaml");
                
                //yield return new Uri("pack://application:,,,/MinoriEditorStudio;component/Themes/VS2013/DarkTheme.xaml");
            }
        }
    }
}
