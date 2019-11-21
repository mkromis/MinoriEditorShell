using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorShell.Services;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Commands
{
    [MesCommandHandler]
    public class MesViewHistoryCommandHandler : CommandHandlerBase<MesViewHistoryCommandDefinition>
    {
        private readonly IMesManager _shell;

        [ImportingConstructor]
        public MesViewHistoryCommandHandler(IMesManager shell)
        {
            _shell = shell;
        }

        public override Task Run(MesCommand command)
        {
            _shell.ShowTool<IMesHistoryTool>();
            return MesTaskUtility.Completed;
        }
    }
}
