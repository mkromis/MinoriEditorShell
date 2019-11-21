using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandDefinition]
    public class MesUndoCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "Edit.Undo";

        public override String Name => CommandName;

        public override String Text => Resources.EditUndoCommandText;

        public override String ToolTip => Resources.EditUndoCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Undo.png");

        [Export]
        public static MesCommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<MesUndoCommandDefinition>(new KeyGesture(Key.Z, ModifierKeys.Control));
    }
}
