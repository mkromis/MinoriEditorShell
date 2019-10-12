using System.Threading.Tasks;
using System.Windows;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [CommandHandler]
    public class ViewFullScreenCommandHandler : CommandHandlerBase<ViewFullScreenCommandDefinition>
    {
        public override Task Run(Command command)
        {
            Window window = Application.Current.MainWindow;
            if (window == null)
            {
                return TaskUtility.Completed;
            }

            window.WindowState = window.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
            return TaskUtility.Completed;
        }
    }
}
