using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Properties;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class SaveAllFilesCommandDefinition : CommandDefinition
    {
        public const String CommandName = "File.SaveAllFiles";

        public override String Name => CommandName;

        public override String Text => Resources.FileSaveAllCommandText;

        public override String ToolTip => Resources.FileSaveAllCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/SaveAll.png");

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<SaveAllFilesCommandDefinition>(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift));
    }
}
