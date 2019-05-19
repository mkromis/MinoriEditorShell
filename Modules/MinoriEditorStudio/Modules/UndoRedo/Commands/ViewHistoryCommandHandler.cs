using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Framework.Threading;

namespace MinoriEditorStudio.Modules.UndoRedo.Commands
{
    [CommandHandler]
    public class ViewHistoryCommandHandler : CommandHandlerBase<ViewHistoryCommandDefinition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public ViewHistoryCommandHandler(IShell shell)
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
