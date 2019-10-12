using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Models
{
    public class CommandMenuItem : StandardMenuItem, ICommandUiItem
    {
        private readonly Command _command;
        private readonly KeyGesture _keyGesture;
        private readonly StandardMenuItem _parent;
        private readonly List<StandardMenuItem> _listItems;

        public override String Text => _command.Text;

        public override Uri IconSource => _command.IconSource;

        public override String InputGestureText
        {
            get
            {
                return _keyGesture == null
                    ? String.Empty
                    : _keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);
            }
        }

        public override ICommand Command => Mvx.IoCProvider.Resolve<ICommandService>().GetTargetableCommand(_command);

        public override Boolean IsChecked => _command.Checked;

        public override Boolean IsVisible => _command.Visible;

        private bool IsListItem { get; set; }

        public CommandMenuItem(Command command, StandardMenuItem parent)
        {
            _command = command;
            _keyGesture = Mvx.IoCProvider.Resolve<ICommandKeyGestureService>().GetPrimaryKeyGesture(_command.CommandDefinition);
            _parent = parent;

            _listItems = new List<StandardMenuItem>();
        }

        CommandDefinitionBase ICommandUiItem.CommandDefinition => _command.CommandDefinition;

        void ICommandUiItem.Update(CommandHandlerWrapper commandHandler)
        {
            if (_command != null && _command.CommandDefinition.IsList && !IsListItem)
            {
                foreach (StandardMenuItem listItem in _listItems)
                {
                    _parent.Children.Remove(listItem);
                }

                _listItems.Clear();

                List<Command> listCommands = new List<Command>();
                commandHandler.Populate(_command, listCommands);

                _command.Visible = false;

                Int32 startIndex = _parent.Children.IndexOf(this) + 1;

                foreach (Command command in listCommands)
                {
                    CommandMenuItem newMenuItem = new CommandMenuItem(command, _parent)
                    {
                        IsListItem = true
                    };
                    _parent.Children.Insert(startIndex++, newMenuItem);
                    _listItems.Add(newMenuItem);
                }
            }
        }
    }
}
