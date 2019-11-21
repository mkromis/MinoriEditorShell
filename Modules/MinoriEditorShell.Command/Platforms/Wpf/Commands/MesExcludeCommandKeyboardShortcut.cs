namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public class MesExcludeCommandKeyboardShortcut
    {
        private readonly MesCommandKeyboardShortcut _keyboardShortcut;

        public MesCommandKeyboardShortcut KeyboardShortcut
        {
            get { return _keyboardShortcut; }
        }

        public MesExcludeCommandKeyboardShortcut(MesCommandKeyboardShortcut keyboardShortcut)
        {
            _keyboardShortcut = keyboardShortcut;
        }
    }
}
