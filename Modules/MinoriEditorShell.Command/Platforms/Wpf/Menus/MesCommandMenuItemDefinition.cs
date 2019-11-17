using System;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;
using MinoriEditorShell.Platforms.Wpf.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Menus
{
    public class MesCommandMenuItemDefinition<TCommandDefinition> : MesMenuItemDefinition
        where TCommandDefinition : MesCommandDefinitionBase
    {
        private readonly MesCommandDefinitionBase _commandDefinition;
        private readonly KeyGesture _keyGesture;

        public override String Text => _commandDefinition.Text;

        public override Uri IconSource => _commandDefinition.IconSource;

        public override KeyGesture KeyGesture => _keyGesture;

        public override MesCommandDefinitionBase CommandDefinition => _commandDefinition;

        public MesCommandMenuItemDefinition(MesMenuItemGroupDefinition group, int sortOrder)
            : base(group, sortOrder)
        {
            _commandDefinition = Mvx.IoCProvider.Resolve<IMesCommandService>().GetCommandDefinition(typeof(TCommandDefinition));
            _keyGesture = Mvx.IoCProvider.Resolve<IMesCommandKeyGestureService>().GetPrimaryKeyGesture(_commandDefinition);
        }
    }
}
