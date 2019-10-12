using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class OpenFileCommandDefinition : CommandDefinition
    {
        public const String CommandName = "File.OpenFile";

        public override String Name => CommandName;

        public override String Text => Resources.FileOpenCommandText;

        public override String ToolTip => Resources.FileOpenCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/Open.png");

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<OpenFileCommandDefinition>(new KeyGesture(Key.O, ModifierKeys.Control));
    }
}
