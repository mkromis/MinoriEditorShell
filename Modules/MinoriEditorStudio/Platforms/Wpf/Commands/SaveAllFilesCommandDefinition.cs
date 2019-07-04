using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Properties;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class SaveAllFilesCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.SaveAllFiles";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.FileSaveAllCommandText; }
        }

        public override string ToolTip
        {
            get { return Resources.FileSaveAllCommandToolTip; }
        }

        public override Uri IconSource
        {
            get 
            {
                return new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/SaveAll.png"); 
            }
        }

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<SaveAllFilesCommandDefinition>(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift));
    }
}
