using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Modules.UndoRedo.Commands
{
    [CommandDefinition]
    public class RedoCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Edit.Redo";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.EditRedoCommandText; }
        }

        public override string ToolTip
        {
            get { return Resources.EditRedoCommandToolTip; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Redo.png"); }
        }

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<RedoCommandDefinition>(new KeyGesture(Key.Y, ModifierKeys.Control));
    }
}
