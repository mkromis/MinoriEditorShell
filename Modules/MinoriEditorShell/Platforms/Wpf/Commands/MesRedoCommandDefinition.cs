using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandDefinition]
    public class MesRedoCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "Edit.Redo";

        public override String Name => CommandName;

        public override String Text => Resources.EditRedoCommandText;

        public override String ToolTip => Resources.EditRedoCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Redo.png");

        [Export]
        public static MesCommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<MesRedoCommandDefinition>(new KeyGesture(Key.Y, ModifierKeys.Control));
    }
}
