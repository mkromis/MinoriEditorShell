using System;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    /// <summary>
    /// Dark theme definition
    /// </summary>
    public class MesDarkTheme : MesThemeBase
    {
        /// <summary>
        /// Name of dark theme
        /// </summary>
        public override String Name => Properties.Resources.ThemeDarkName;

        /// <summary>
        /// Constructor for theme
        /// </summary>
        public MesDarkTheme()
        {
            Add(new Uri("pack://application:,,,/MinoriEditorShell.Platforms.Wpf;component/Themes/DarkTheme.xaml"));
        }
    }
}
