using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    [CommandDefinition]
    public class ViewFullScreenCommandDefinition : CommandDefinition
    {
        public const String CommandName = "View.FullScreen";

        public override String Name => CommandName;

        public override String Text => Resources.ViewFullScreenCommandText;

        public override String ToolTip => Resources.ViewFullScreenCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/MinoriEditorStudio;component/Resources/Icons/FullScreen.png");

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<ViewFullScreenCommandDefinition>(new KeyGesture(Key.Enter, ModifierKeys.Shift | ModifierKeys.Alt));
    }
}
