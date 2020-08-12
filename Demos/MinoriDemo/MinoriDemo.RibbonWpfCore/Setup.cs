using MinoriEditorShell.Platforms.Wpf;
using MinoriEditorShell.Platforms.Wpf.Themes;
using MinoriEditorShell.Services;
using MvvmCross;
using System;

namespace MinoriDemo.RibbonWpfCore
{
    internal class Setup : MesWpfSetup<Core.App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            IMesThemeManager manager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            foreach (IMesTheme theme in manager.Themes)
            {
                switch (theme)
                {
                    case MesBlueTheme blue:
                        blue.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/LightBrushs.xaml"));
                        break;

                    case MesLightTheme light:
                        light.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/LightBrushs.xaml"));
                        break;

                    case MesDarkTheme dark:
                        dark.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/DarkBrushs.xaml"));
                        break;

                    default:
                        break;
                }
            }
        }
    }
}