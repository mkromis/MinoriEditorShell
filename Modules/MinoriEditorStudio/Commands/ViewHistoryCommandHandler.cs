using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.Threading;

namespace MinoriEditorStudio.Commands
{
    [CommandHandler]
    public class ViewHistoryCommandHandler : CommandHandlerBase<ViewHistoryCommandDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public ViewHistoryCommandHandler(IManager shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.ShowTool<IHistoryTool>();
            return TaskUtility.Completed;
        }
    }
}
