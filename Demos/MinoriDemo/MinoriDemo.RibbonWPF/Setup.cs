using MinoriEditorShell.Platforms.Wpf;
using MinoriEditorShell.Platforms.Wpf.Themes;
using MinoriEditorShell.Services;
using MvvmCross;
using System;

namespace MinoriDemo.RibbonWPF
{
    internal class Setup : MesWpfSetup<Core.App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            IThemeManager manager = Mvx.IoCProvider.Resolve<IThemeManager>();
            foreach (ITheme theme in manager.Themes)
            {
                switch (theme)
                {
                    case BlueTheme blue:
                        blue.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/LightBrushs.xaml"));
                        break;
                    case LightTheme light:
                        light.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/LightBrushs.xaml"));
                        break;
                    case DarkTheme dark:
                        dark.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/DarkBrushs.xaml"));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}