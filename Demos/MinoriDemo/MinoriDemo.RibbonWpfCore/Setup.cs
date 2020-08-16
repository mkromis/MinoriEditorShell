using MinoriEditorShell.Platforms.Wpf;
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

            IMesThemeManager manager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            foreach (IMesTheme theme in manager.Themes)
            {
                theme.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/Generic.xaml"));
            }
        }
    }
}