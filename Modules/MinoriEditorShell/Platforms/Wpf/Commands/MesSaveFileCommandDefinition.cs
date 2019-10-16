using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandDefinition]
    public class MesSaveFileCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "File.SaveFile";

        public override String Name => CommandName;

        public override String Text => Resources.FileSaveCommandText;

        public override String ToolTip => Resources.FileSaveCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Save.png");

        [Export]
        public static MesCommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<MesSaveFileCommandDefinition>(new KeyGesture(Key.S, ModifierKeys.Control));
    }
}
