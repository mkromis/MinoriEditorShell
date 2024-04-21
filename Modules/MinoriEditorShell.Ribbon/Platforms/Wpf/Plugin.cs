using MinoriEditorShell.Platforms.Wpf.Themes;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using System;

namespace MinoriEditorShell.Ribbon.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load(IMvxIoCProvider provider)
        {
            // for repeatable ribbon items
            IMesThemeManager thememanager = provider.Resolve<IMesThemeManager>();
            foreach (IMesTheme theme in thememanager.Themes)
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