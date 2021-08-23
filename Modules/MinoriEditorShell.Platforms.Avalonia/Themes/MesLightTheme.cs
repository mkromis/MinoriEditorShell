using System;

namespace MinoriEditorShell.Platforms.Avalonia.Themes
{
    /// <summary>
    /// Light theme definition
    /// </summary>
    public class MesLightTheme : MesThemeBase
    {
        /// <summary>
        /// Name of the light theme
        /// </summary>
        public override String Name => Properties.Resources.ThemeLightName;

        /// <summary>
        /// Light theme constructor
        /// </summary>
        public MesLightTheme() : base()
        { }
            //=> Add(new Uri("pack://application:,,,/MinoriEditorShell.Platforms.Wpf;component/Themes/LightTheme.xaml"));
    }
}