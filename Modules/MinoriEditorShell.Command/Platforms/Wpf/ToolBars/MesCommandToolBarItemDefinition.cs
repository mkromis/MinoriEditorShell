using System;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public class MesCommandToolBarItemDefinition<TCommandDefinition> : MesToolBarItemDefinition
        where TCommandDefinition : MesCommandDefinitionBase
    {
        private readonly MesCommandDefinitionBase _commandDefinition;
        private readonly KeyGesture _keyGesture;

        public override String Text => _commandDefinition.ToolTip;

        public override Uri IconSource => _commandDefinition.IconSource;

        public override KeyGesture KeyGesture => _keyGesture;

        public override MesCommandDefinitionBase CommandDefinition => _commandDefinition;

        public MesCommandToolBarItemDefinition(MesToolBarItemGroupDefinition group, int sortOrder, ToolBarItemDisplay display = ToolBarItemDisplay.IconOnly)
            : base(group, sortOrder, display)
        {
            _commandDefinition = Mvx.IoCProvider.Resolve<IMesCommandService>().GetCommandDefinition(typeof(TCommandDefinition));
            _keyGesture = Mvx.IoCProvider.Resolve<IMesCommandKeyGestureService>().GetPrimaryKeyGesture(_commandDefinition);
        }
    }
}
