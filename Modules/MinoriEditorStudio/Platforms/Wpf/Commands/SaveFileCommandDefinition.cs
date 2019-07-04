using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class SaveFileCommandDefinition : CommandDefinition
    {
        public const String CommandName = "File.SaveFile";

        public override String Name => CommandName;

        public override String Text => Resources.FileSaveCommandText;

        public override String ToolTip => Resources.FileSaveCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Save.png");

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<SaveFileCommandDefinition>(new KeyGesture(Key.S, ModifierKeys.Control));
    }
}
