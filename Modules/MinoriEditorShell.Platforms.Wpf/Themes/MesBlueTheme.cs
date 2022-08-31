using System;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    /// <summary>
    /// Blue Theme
    /// </summary>
    public class MesBlueTheme : MesThemeBase
    {
        /// <summary>
        /// Name of theme
        /// </summary>
        public override string Name => Properties.Resources.ThemeBlueName;

        /// <summary>
        /// Blue theme constructor
        /// </summary>
        public MesBlueTheme() : base() => Add(new Uri("pack://application:,,,/MinoriEditorShell.Platforms.Wpf;component/Themes/BlueTheme.xaml"));
    }
}