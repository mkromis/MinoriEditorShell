using MinoriEditorShell.Commands;
using MinoriEditorShell.Properties;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandDefinition]
    public class MesSaveAllFilesCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "File.SaveAllFiles";

        public override String Name => CommandName;

        public override String Text => Resources.FileSaveAllCommandText;

        public override String ToolTip => Resources.FileSaveAllCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/SaveAll.png");

        [Export]
        public static MesCommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<MesSaveAllFilesCommandDefinition>(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift));
    }
}
