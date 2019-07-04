using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.Threading;

namespace MinoriEditorStudio.Commands
{
    [CommandHandler]
    public class ViewToolboxCommandHandler : CommandHandlerBase<ViewToolboxCommandDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public ViewToolboxCommandHandler(IManager shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.ShowTool<IToolbox>();
            return TaskUtility.Completed;
        }
    }
}
