using System;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public class CommandToolBarItemDefinition<TCommandDefinition> : ToolBarItemDefinition
        where TCommandDefinition : CommandDefinitionBase
    {
        private readonly CommandDefinitionBase _commandDefinition;
        private readonly KeyGesture _keyGesture;

        public override String Text => _commandDefinition.ToolTip;

        public override Uri IconSource => _commandDefinition.IconSource;

        public override KeyGesture KeyGesture => _keyGesture;

        public override CommandDefinitionBase CommandDefinition => _commandDefinition;

        public CommandToolBarItemDefinition(ToolBarItemGroupDefinition group, int sortOrder, ToolBarItemDisplay display = ToolBarItemDisplay.IconOnly)
            : base(group, sortOrder, display)
        {
            _commandDefinition = Mvx.IoCProvider.Resolve<ICommandService>().GetCommandDefinition(typeof(TCommandDefinition));
            _keyGesture = Mvx.IoCProvider.Resolve<ICommandKeyGestureService>().GetPrimaryKeyGesture(_commandDefinition);
        }
    }
}
