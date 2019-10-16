using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Services;
using MvvmCross;
using System;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public abstract class MesCommandKeyboardShortcut
    {
        private readonly Func<MesCommandDefinitionBase> _commandDefinition;

        public MesCommandDefinitionBase CommandDefinition => _commandDefinition();

        public KeyGesture KeyGesture { get; }

        public Int32 SortOrder { get; }

        protected MesCommandKeyboardShortcut(KeyGesture keyGesture, Int32 sortOrder, Func<MesCommandDefinitionBase> commandDefinition)
        {
            _commandDefinition = commandDefinition;
            KeyGesture = keyGesture;
            SortOrder = sortOrder;
        }
    }

    public class CommandKeyboardShortcut<TCommandDefinition> : MesCommandKeyboardShortcut
        where TCommandDefinition : MesCommandDefinition
    {
        public CommandKeyboardShortcut(KeyGesture keyGesture, Int32 sortOrder = 5)
            : base(keyGesture, sortOrder, () => Mvx.IoCProvider.Resolve<IMesCommandService>().GetCommandDefinition(typeof(TCommandDefinition)))
        {
            
        }
    }
}
