using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Services;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [CommandHandler]
    public class ExitCommandHandler : CommandHandlerBase<ExitCommandDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public ExitCommandHandler(IManager shell) => _shell = shell;

        public override Task Run(Command command)
        {
            _shell.Close();
            return TaskUtility.Completed;
        }
    }
}
