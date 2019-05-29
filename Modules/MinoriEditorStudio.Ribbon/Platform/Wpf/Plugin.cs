using MinoriEditorStudio.Modules.Themes.Services;
using MinoriEditorStudio.Ribbon.Platform.Wpf.Controls;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorStudio.Ribbon.Platform.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            // Setup manager, is there a better way?
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeList>(() => new ThemeList
            {
                new DefaultTheme(),
                new BlueTheme(),
                new LightTheme(),
                new DarkTheme(),
            }); ;
        }
    }
}
