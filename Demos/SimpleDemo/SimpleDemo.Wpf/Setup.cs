using Microsoft.Extensions.Logging;
using MinoriEditorShell.Platforms.Wpf;

namespace SimpleDemo.WpfCore
{
    internal class Setup : MesWpfSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory() =>  null;
        protected override ILoggerProvider CreateLogProvider() => null;
    }
}