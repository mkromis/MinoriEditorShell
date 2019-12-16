using MinoriEditorShell.Extensions;
using MinoriEditorShell.Platforms.Wpf.Themes;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Plugin;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Ribbon.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            // for repeatable ribbon items
            IMesThemeManager thememanager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            foreach (var theme in thememanager.Themes)
            {
                switch (theme)
                {
                    case MesBlueTheme blue:
                        blue.Add(new Uri("pack://application:,,,/MinoriEditorShell.Ribbon;component/Platforms/Wpf/Themes/BlueTheme.xaml"));
                        break;

                    case MesLightTheme light:
                        light.Add(new Uri("pack://application:,,,/MinoriEditorShell.Ribbon;component/Platforms/Wpf/Themes/LightTheme.xaml"));
                        break;

                    case MesDarkTheme dark:
                        dark.Add(new Uri("pack://application:,,,/MinoriEditorShell.Ribbon;component/Platforms/Wpf/Themes/DarkTheme.xaml"));
                        break;
                }
            }
        }
    }
}
