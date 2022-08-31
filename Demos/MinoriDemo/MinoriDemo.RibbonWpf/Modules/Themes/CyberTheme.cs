using MinoriEditorShell.Platforms.Wpf.Themes;
using System;

namespace MinoriDemo.RibbonWPF.Modules.Themes
{
    internal class CyberTheme : MesThemeBase
    {
        /// <summary>
        /// When doing this in your own project, try to localize if if necessary
        /// </summary>
        public override string Name => "CyberTheme";

        public CyberTheme() : base()
        {
            Add(new Uri("pack://application:,,,/MinoriDemo.RibbonWPF;component/Modules/Themes/CyberTheme.xaml"));
        }
    }
}