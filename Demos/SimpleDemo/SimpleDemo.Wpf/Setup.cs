using Microsoft.Extensions.Logging;
using MinoriEditorShell.Platforms.Wpf;
using MvvmCross.Platforms.Wpf.Core;

namespace SimpleDemo.Wpf
{
    public class Setup : MesWpfSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory()
        {
            return LoggerFactory.Create(builder => builder.SetMinimumLevel(0).AddDebug());
        }

        protected override ILoggerProvider CreateLogProvider() => null;
    }
}