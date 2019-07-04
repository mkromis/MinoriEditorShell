using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Modules.Platforms.Wpf.Commands;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.Threading;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
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
