using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Services;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandHandler]
    public class MesExitCommandHandler : CommandHandlerBase<MesExitCommandDefinition>
    {
        private readonly IMesManager _shell;

        [ImportingConstructor]
        public MesExitCommandHandler(IMesManager shell) => _shell = shell;

        public override Task Run(MesCommand command)
        {
            _shell.Close();
            return MesTaskUtility.Completed;
        }
    }
}
