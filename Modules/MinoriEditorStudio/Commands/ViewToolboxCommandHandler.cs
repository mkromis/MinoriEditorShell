using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Framework.Threading;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.Modules.Toolbox.Commands
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
