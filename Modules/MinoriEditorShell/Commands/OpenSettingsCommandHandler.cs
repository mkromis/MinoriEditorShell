using System.Threading.Tasks;

namespace MinoriEditorShell.Commands
{
    [CommandHandler]
    public class OpenSettingsCommandHandler : CommandHandlerBase<OpenSettingsCommandDefinition>
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
        public override Task Run(Command command) => throw new System.NotImplementedException();
    }
}
