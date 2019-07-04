using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Platforms.Wpf.Commands;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Modules.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class ExitCommandDefinition : CommandDefinition
    {
        public const String CommandName = "File.Exit";

        public override String Name => CommandName;

        public override String Text => Resources.FileExitCommandText;

        public override String ToolTip => Resources.FileExitCommandToolTip;

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<ExitCommandDefinition>(new KeyGesture(Key.F4, ModifierKeys.Alt));
    }
}
