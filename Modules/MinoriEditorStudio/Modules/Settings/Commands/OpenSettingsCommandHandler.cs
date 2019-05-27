using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.Threading;
using MinoriEditorStudio.Modules.Settings.ViewModels;

namespace MinoriEditorStudio.Modules.Settings.Commands
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
