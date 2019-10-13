using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    public class BlueTheme : ThemeBase
    {
        public override String Name => Properties.Resources.ThemeBlueName;


        public BlueTheme() : base()
        {
            AddRange(new List<Uri> {
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml"),

                new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/BlueTheme.xaml"),
                new Uri("pack://application:,,,/MinoriEditorShell;component/Platforms/Wpf/Themes/VS2013/BlueTheme.xaml"),
            });
        }
    }
}
