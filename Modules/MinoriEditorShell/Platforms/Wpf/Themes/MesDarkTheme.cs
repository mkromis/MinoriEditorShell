using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    public class MesDarkTheme : MesThemeBase
    {
        public override String Name => Properties.Resources.ThemeDarkName;

        public MesDarkTheme()
        {
            AddRange( new List<Uri> {
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml"),

                new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/DarkTheme.xaml"),
                
                //yield return new Uri("pack://application:,,,/MinoriEditorShell;component/Themes/VS2013/DarkTheme.xaml");
            });
        }
    }
}
