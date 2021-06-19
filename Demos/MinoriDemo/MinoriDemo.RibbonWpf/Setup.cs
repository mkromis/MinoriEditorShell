using Microsoft.Extensions.Logging;
using MinoriEditorShell.Platforms.Wpf;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;

namespace MinoriDemo.RibbonWPF
{
    internal class Setup : MesWpfSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory() =>
            LoggerFactory.Create((builder) =>
                builder
                    .SetMinimumLevel(0)
                    .AddDebug());

        protected override ILoggerProvider CreateLogProvider() => null;

        protected override void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeLastChance(iocProvider);

            IMesThemeManager manager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            foreach (IMesTheme theme in manager.Themes)
            {
                theme.Add(new Uri("pack://application:,,,/ColorPickerLib;component/Themes/Generic.xaml"));
            }
        }
    }
}