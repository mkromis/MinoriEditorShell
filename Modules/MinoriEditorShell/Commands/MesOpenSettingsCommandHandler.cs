using System.Threading.Tasks;

namespace MinoriEditorShell.Commands
{
    [MesCommandHandler]
    public class MesOpenSettingsCommandHandler : CommandHandlerBase<MesOpenSettingsCommandDefinition>
    {
#warning IWindowManager
#if false
        private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public OpenSettingsCommandHandler(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public override Task Run(Command command)
        {
            _windowManager.ShowDialog(IoC.Get<SettingsViewModel>());
            return TaskUtility.Completed;
        }
#endif
        public override Task Run(MesCommand command) => throw new System.NotImplementedException();
    }
}
