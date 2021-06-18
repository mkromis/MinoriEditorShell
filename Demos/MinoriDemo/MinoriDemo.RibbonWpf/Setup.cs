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
        protected override IMvxApplication CreateApp(IMvxIoCProvider iocProvider) => throw new NotImplementedException();
        protected override ILoggerFactory CreateLogFactory() => throw new NotImplementedException();
        protected override ILoggerProvider CreateLogProvider() => throw new NotImplementedException();

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