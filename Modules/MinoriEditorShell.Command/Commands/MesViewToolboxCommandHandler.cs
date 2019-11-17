using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorShell.Services;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Commands
{
    [MesCommandHandler]
    public class MesViewToolboxCommandHandler : CommandHandlerBase<MesViewToolboxCommandDefinition>
    {
        private readonly IMesManager _shell;

        [ImportingConstructor]
        public MesViewToolboxCommandHandler(IMesManager shell)
        {
            _shell = shell;
        }

        public override Task Run(MesCommand command)
        {
            _shell.ShowTool<IMesToolbox>();
            return MesTaskUtility.Completed;
        }
    }
}
