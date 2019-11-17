using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandDefinition]
    public class MesExitCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "File.Exit";

        public override String Name => CommandName;

        public override String Text => Resources.FileExitCommandText;

        public override String ToolTip => Resources.FileExitCommandToolTip;

        [Export]
        public static MesCommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<MesExitCommandDefinition>(new KeyGesture(Key.F4, ModifierKeys.Alt));
    }
}
