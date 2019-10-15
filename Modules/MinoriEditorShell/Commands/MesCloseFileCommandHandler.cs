using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorShell.Services;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Commands
{
    [MesCommandHandler]
    public class MesCloseFileCommandHandler : CommandHandlerBase<MesCloseFileCommandDefinition>
    {
        private readonly IMesManager _shell;

        [ImportingConstructor]
        public MesCloseFileCommandHandler(IMesManager shell) => _shell = shell;

        public override void Update(MesCommand command)
        {
            command.Enabled = _shell.ActiveItem != null;
            base.Update(command);
        }

        public override Task Run(MesCommand command)
        {
            _shell.CloseDocument(_shell.SelectedDocument);
            return MesTaskUtility.Completed;
        }
    }
}
