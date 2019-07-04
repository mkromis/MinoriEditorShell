using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class RedoCommandDefinition : CommandDefinition
    {
        public const String CommandName = "Edit.Redo";

        public override String Name => CommandName;

        public override String Text => Resources.EditRedoCommandText;

        public override String ToolTip => Resources.EditRedoCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Redo.png");

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<RedoCommandDefinition>(new KeyGesture(Key.Y, ModifierKeys.Control));
    }
}
