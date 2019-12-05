using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    public class MesBlueTheme : MesThemeBase
    {
        public override String Name => Properties.Resources.ThemeBlueName;


        public MesBlueTheme() : base()
        {
            AddRange(new List<Uri> {
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/VS/Styles.xaml"),

                //new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Themes/Light.Blue.xaml"),

                //new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/BlueTheme.xaml"),
                //new Uri("pack://application:,,,/MinoriEditorShell;component/Platforms/Wpf/Themes/VS2013/BlueTheme.xaml"),
            });
        }
    }
}
