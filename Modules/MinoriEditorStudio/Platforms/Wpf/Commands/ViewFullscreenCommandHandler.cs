using System.Threading.Tasks;
using System.Windows;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.Threading;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    [CommandHandler]
    public class ViewFullScreenCommandHandler : CommandHandlerBase<ViewFullScreenCommandDefinition>
    {
        public override Task Run(Command command)
        {
            var window = Application.Current.MainWindow;
            if (window == null)
                return TaskUtility.Completed;
            if (window.WindowState != WindowState.Maximized)
                window.WindowState = WindowState.Maximized;
            else
                window.WindowState = WindowState.Normal;
            return TaskUtility.Completed;
        }
    }
}
