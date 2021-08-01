using Microsoft.Extensions.Logging;
using MinoriEditorShell.Platforms.Avalonia;

namespace SimpleDemo.Avalonia
{
    internal class Setup : MesAvnSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory() => LoggerFactory.Create(b => b.SetMinimumLevel(0).AddSimpleConsole(o => o.SingleLine = true));
        protected override ILoggerProvider CreateLogProvider() => null;
    }
}