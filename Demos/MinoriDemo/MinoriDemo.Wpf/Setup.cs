using Microsoft.Extensions.Logging;
using MinoriEditorShell.Platforms.Wpf;

namespace MinoriDemo.WpfCore
{
    internal class Setup : MesWpfSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory() => throw new System.NotImplementedException();
        protected override ILoggerProvider CreateLogProvider() => throw new System.NotImplementedException();
    }
}