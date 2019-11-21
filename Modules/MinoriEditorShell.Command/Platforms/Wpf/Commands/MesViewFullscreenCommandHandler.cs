using System.Threading.Tasks;
using System.Windows;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandHandler]
    public class ViewFullScreenCommandHandler : CommandHandlerBase<ViewFullScreenCommandDefinition>
    {
        public override Task Run(MesCommand command)
        {
            Window window = Application.Current.MainWindow;
            if (window == null)
            {
                return MesTaskUtility.Completed;
            }

            window.WindowState = window.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
            return MesTaskUtility.Completed;
        }
    }
}
