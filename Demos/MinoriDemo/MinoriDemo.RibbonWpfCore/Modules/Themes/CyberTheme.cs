using MinoriEditorShell.Platforms.Wpf.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriDemo.RibbonWpfCore.Modules.Themes
{
    class CyberTheme : MesThemeBase
    {
        /// <summary>
        /// When doing this in your own project, try to localize if if necessary
        /// </summary>
        public override String Name => "CyberTheme";

        public CyberTheme() : base()
        {
            Add(new Uri("pack://application:,,,/MinoriDemo.RibbonWPF;component/Modules/Themes/CyberTheme.xaml"));
        }
    }
}
