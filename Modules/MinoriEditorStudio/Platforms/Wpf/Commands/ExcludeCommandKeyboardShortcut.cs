namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    public class ExcludeCommandKeyboardShortcut
    {
        private readonly CommandKeyboardShortcut _keyboardShortcut;

        public CommandKeyboardShortcut KeyboardShortcut
        {
            get { return _keyboardShortcut; }
        }

        public ExcludeCommandKeyboardShortcut(CommandKeyboardShortcut keyboardShortcut)
        {
            _keyboardShortcut = keyboardShortcut;
        }
    }
}
