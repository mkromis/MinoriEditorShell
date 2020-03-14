using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    public class MesLightTheme : MesThemeBase
    {
        public override String Name => Properties.Resources.ThemeLightName;

        public MesLightTheme() : base()
        {
            Add(new Uri("pack://application:,,,/MinoriEditorShell.Platforms.Wpf;component/Themes/LightTheme.xaml"));
        }
    }
}
