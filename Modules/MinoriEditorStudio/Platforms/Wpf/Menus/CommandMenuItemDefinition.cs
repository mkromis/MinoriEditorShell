using System;
using System.Windows.Input;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Platforms.Wpf.Commands;
using MinoriEditorStudio.Platforms.Wpf.MenuDefinitions;
using MinoriEditorStudio.Platforms.Wpf.Services;
using MvvmCross;

namespace MinoriEditorStudio.Platforms.Wpf.Menus
{
    public class CommandMenuItemDefinition<TCommandDefinition> : MenuItemDefinition
        where TCommandDefinition : CommandDefinitionBase
    {
        private readonly CommandDefinitionBase _commandDefinition;
        private readonly KeyGesture _keyGesture;

        public override String Text => _commandDefinition.Text;

        public override Uri IconSource => _commandDefinition.IconSource;

        public override KeyGesture KeyGesture => _keyGesture;

        public override CommandDefinitionBase CommandDefinition => _commandDefinition;

        public CommandMenuItemDefinition(MenuItemGroupDefinition group, int sortOrder)
            : base(group, sortOrder)
        {
            _commandDefinition = Mvx.IoCProvider.Resolve<ICommandService>().GetCommandDefinition(typeof(TCommandDefinition));
            _keyGesture = Mvx.IoCProvider.Resolve<ICommandKeyGestureService>().GetPrimaryKeyGesture(_commandDefinition);
        }
    }
}
