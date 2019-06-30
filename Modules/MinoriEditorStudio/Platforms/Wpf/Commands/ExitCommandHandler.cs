using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Framework.Threading;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.Modules.Shell.Commands
{
    [CommandHandler]
    public class ExitCommandHandler : CommandHandlerBase<ExitCommandDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public ExitCommandHandler(IManager shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.Close();
            return TaskUtility.Completed;
        }
    }
}
