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
    public class MesCommandMenuItem : MesStandardMenuItem, IMesCommandUiItem
    {
        private readonly MesCommand _command;
        private readonly KeyGesture _keyGesture;
        private readonly MesStandardMenuItem _parent;
        private readonly List<MesStandardMenuItem> _listItems;

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

        public override ICommand Command => Mvx.IoCProvider.Resolve<IMesCommandService>().GetTargetableCommand(_command);

        public override Boolean IsChecked => _command.Checked;

        public override Boolean IsVisible => _command.Visible;

        private bool IsListItem { get; set; }

        public MesCommandMenuItem(MesCommand command, MesStandardMenuItem parent)
        {
            _command = command;
            _keyGesture = Mvx.IoCProvider.Resolve<IMesCommandKeyGestureService>().GetPrimaryKeyGesture(_command.CommandDefinition);
            _parent = parent;

            _listItems = new List<MesStandardMenuItem>();
        }

        MesCommandDefinitionBase IMesCommandUiItem.CommandDefinition => _command.CommandDefinition;

        void IMesCommandUiItem.Update(MesCommandHandlerWrapper commandHandler)
        {
            if (_command != null && _command.CommandDefinition.IsList && !IsListItem)
            {
                foreach (MesStandardMenuItem listItem in _listItems)
                {
                    _parent.Children.Remove(listItem);
                }

                _listItems.Clear();

                List<MesCommand> listCommands = new List<MesCommand>();
                commandHandler.Populate(_command, listCommands);

                _command.Visible = false;

                Int32 startIndex = _parent.Children.IndexOf(this) + 1;

                foreach (MesCommand command in listCommands)
                {
                    MesCommandMenuItem newMenuItem = new MesCommandMenuItem(command, _parent)
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
