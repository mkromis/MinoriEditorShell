using System;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Extensions
{
    public static class ConsoleModifiersExtension
    {
        public static ModifierKeys ToModifierKeys(this ConsoleModifiers console)
        {
            switch (console)
            {
                case ConsoleModifiers.Alt:
                    return ModifierKeys.Alt;

                case ConsoleModifiers.Shift:
                    return ModifierKeys.Shift;

                case ConsoleModifiers.Control:
                    return ModifierKeys.Control;

                default:
                    return ModifierKeys.None;
            }
        }
    }
}