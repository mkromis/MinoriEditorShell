using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    public class LightTheme : ThemeBase
    {
        List<Uri> _resources; 

        public override String Name => Properties.Resources.ThemeLightName;


        public LightTheme() : base()
        {
            AddRange(new List<Uri> {
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml"),
                new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/LightTheme.xaml"),

                //new Uri("pack://application:,,,/MinoriEditorShell;component/Themes/VS2013/LightTheme.xaml");
            });
        }
    }
}
