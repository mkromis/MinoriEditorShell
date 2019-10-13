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
            manager.EnsurePluginLoaded<MinoriEditorShell.Platforms.Wpf.Plugin>();

            // for repeatable ribbon items
            Uri[] mainribbon = new Uri[]
            {
                new Uri("pack://application:,,,/Fluent;Component/Themes/Generic.xaml"),
                new Uri("pack://application:,,,/Fluent;component/Themes/Accents/Cobalt.xaml"),
            };

            IThemeManager thememanager = Mvx.IoCProvider.Resolve<IThemeManager>();
            foreach (var theme in thememanager.Themes)
            {
                switch (theme)
                {
                    case BlueTheme blue:
                        blue.AddRange(mainribbon);
                        blue.Add(new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseLight.xaml"));
                        break;

                    case LightTheme light:
                        light.AddRange(mainribbon);
                        light.Add(new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseLight.xaml"));
                        break;

                    case DarkTheme dark:
                        dark.AddRange(mainribbon);
                        dark.Add(new Uri("pack://application:,,,/Fluent;component/Themes/Colors/BaseDark.xaml"));
                        break;
                }
            }
        }
    }
}
