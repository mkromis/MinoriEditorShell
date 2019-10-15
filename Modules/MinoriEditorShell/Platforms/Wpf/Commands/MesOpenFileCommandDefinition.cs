using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandDefinition]
    public class MesOpenFileCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "File.OpenFile";

        public override String Name => CommandName;

        public override String Text => Resources.FileOpenCommandText;

        public override String ToolTip => Resources.FileOpenCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Open.png");

        [Export]
        public static MesCommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<MesOpenFileCommandDefinition>(new KeyGesture(Key.O, ModifierKeys.Control));
    }
}
