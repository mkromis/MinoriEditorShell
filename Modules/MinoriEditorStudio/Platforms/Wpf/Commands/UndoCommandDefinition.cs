using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class UndoCommandDefinition : CommandDefinition
    {
        public const String CommandName = "Edit.Undo";

        public override String Name => CommandName;

        public override String Text => Resources.EditUndoCommandText;

        public override String ToolTip => Resources.EditUndoCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Undo.png");

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<UndoCommandDefinition>(new KeyGesture(Key.Z, ModifierKeys.Control));
    }
}
