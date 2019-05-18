using MvvmCross;
using System;
using System.Windows.Input;

namespace MinoriEditorStudio.Framework.Commands
{
    public abstract class CommandKeyboardShortcut
    {
        private readonly Func<CommandDefinitionBase> _commandDefinition;

        public CommandDefinitionBase CommandDefinition => _commandDefinition();

        public KeyGesture KeyGesture { get; }

        public int SortOrder { get; }

        protected CommandKeyboardShortcut(KeyGesture keyGesture, int sortOrder, Func<CommandDefinitionBase> commandDefinition)
        {
            _commandDefinition = commandDefinition;
            KeyGesture = keyGesture;
            SortOrder = sortOrder;
        }
    }

    public class CommandKeyboardShortcut<TCommandDefinition> : CommandKeyboardShortcut
        where TCommandDefinition : CommandDefinition
    {
        public CommandKeyboardShortcut(KeyGesture keyGesture, int sortOrder = 5)
            : base(keyGesture, sortOrder, () => Mvx.IoCProvider.Resolve<ICommandService>().GetCommandDefinition(typeof(TCommandDefinition)))
        {
            
        }
    }
}
