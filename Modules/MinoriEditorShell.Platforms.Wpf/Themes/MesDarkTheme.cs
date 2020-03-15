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
            Add(new Uri("pack://application:,,,/MinoriEditorShell.Platforms.Wpf;component/Themes/DarkTheme.xaml"));
        }
    }
}
