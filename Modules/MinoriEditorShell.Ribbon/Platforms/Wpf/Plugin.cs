﻿using MinoriEditorShell.Extensions;
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
            IMvxPluginManager manager = Mvx.IoCProvider.Resolve<IMvxPluginManager>();

            // for repeatable ribbon items
            Uri[] mainribbon = new Uri[]
            {
                new Uri("pack://application:,,,/Fluent;Component/Themes/Generic.xaml"),
                new Uri("pack://application:,,,/Fluent;component/Themes/Accents/Cobalt.xaml"),
            };

            IMesThemeManager thememanager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            foreach (var theme in thememanager.Themes)
            {
                switch (theme)
                {
                    case MesBlueTheme blue:
                        blue.AddRange(mainribbon);
                        blue.AddRange(new Uri[] {
                            new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseLight.xaml"),
                            new Uri("pack://application:,,,/MinoriEditorShell.Ribbon;component/Platforms/Wpf/Themes/BlueTheme.xaml"),
                        });
                        break;

                    case MesLightTheme light:
                        light.AddRange(mainribbon);
                        light.AddRange(new Uri[] {
                            new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseLight.xaml"),
                            new Uri("pack://application:,,,/MinoriEditorShell.Ribbon;component/Platforms/Wpf/Themes/LightTheme.xaml"),
                        });
                        break;

                    case MesDarkTheme dark:
                        dark.AddRange(mainribbon);
                        dark.AddRange(new Uri[] {
                            new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseDark.xaml"),
                            new Uri("pack://application:,,,/MinoriEditorShell.Ribbon;component/Platforms/Wpf/Themes/DarkTheme.xaml"),
                        });
                        break;
                }
            }
        }
    }
}
