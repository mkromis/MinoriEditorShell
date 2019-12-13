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
            Add(new Uri("pack://application:,,,/MinoriEditorShell;component/Platforms/Wpf/Themes/VS2013/BlueTheme.xaml"));
        }
    }
}
