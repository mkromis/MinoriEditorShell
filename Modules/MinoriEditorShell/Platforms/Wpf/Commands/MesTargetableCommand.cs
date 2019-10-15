using MinoriEditorShell.Commands;
using MvvmCross;
using System;
using System.Windows;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public class MesTargetableCommand : ICommand
    {
        private readonly MesCommand _command;
        private readonly IMesCommandRouter _commandRouter;

        public MesTargetableCommand(MesCommand command)
        {
            _command = command;
            _commandRouter = Mvx.IoCProvider.Resolve<IMesCommandRouter>();
        }

        public Boolean CanExecute(Object parameter)
        {
            MesCommandHandlerWrapper commandHandler = _commandRouter.GetCommandHandler(_command.CommandDefinition);
            if (commandHandler == null)
            {
                return false;
            }

            commandHandler.Update(_command);

            return _command.Enabled;
        }

        public async void Execute(Object parameter)
        {
            MesCommandHandlerWrapper commandHandler = _commandRouter.GetCommandHandler(_command.CommandDefinition);
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
