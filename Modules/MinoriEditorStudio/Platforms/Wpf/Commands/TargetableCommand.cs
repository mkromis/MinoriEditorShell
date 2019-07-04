using MinoriEditorStudio.Commands;
using MvvmCross;
using System;
using System.Windows;
using System.Windows.Input;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    public class TargetableCommand : ICommand
    {
        private readonly Command _command;
        private readonly ICommandRouter _commandRouter;

        public TargetableCommand(Command command)
        {
            _command = command;
            _commandRouter = Mvx.IoCProvider.Resolve<ICommandRouter>();
        }

        public Boolean CanExecute(Object parameter)
        {
            CommandHandlerWrapper commandHandler = _commandRouter.GetCommandHandler(_command.CommandDefinition);
            if (commandHandler == null)
            {
                return false;
            }

            commandHandler.Update(_command);

            return _command.Enabled;
        }

        public async void Execute(Object parameter)
        {
            CommandHandlerWrapper commandHandler = _commandRouter.GetCommandHandler(_command.CommandDefinition);
            if (commandHandler == null)
            {
                return;
            }

            await commandHandler.Run(_command);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
