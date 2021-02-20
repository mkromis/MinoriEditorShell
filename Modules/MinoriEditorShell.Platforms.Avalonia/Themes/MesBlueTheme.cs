using System;

namespace MinoriEditorShell.Platforms.Avalonia.Themes
{
    /// <summary>
    /// Blue Theme
    /// </summary>
    public class MesBlueTheme : MesThemeBase
    {
        /// <summary>
        /// Name of theme
        /// </summary>
        public override String Name => Properties.Resources.ThemeBlueName;

        /// <summary>
        /// Blue theme constructor
        /// </summary>
        public MesBlueTheme() : base() => Add(new Uri("pack://application:,,,/MinoriEditorShell.Platforms.Avalonia;component/Themes/BlueTheme.xaml"));
    }
}