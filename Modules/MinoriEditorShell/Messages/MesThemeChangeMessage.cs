using MvvmCross.Plugin.Messenger;
using System;

namespace MinoriEditorShell.Messages
{
    /// <summary>
    /// Theme change message
    /// </summary>
    public class MesThemeChangeMessage : MvxMessage
    {
        /// <summary>
        /// Theme message constructor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="themeName"></param>
        public MesThemeChangeMessage(Object sender, String themeName) : base(sender)
        {
            ThemeName = themeName;
        }

        /// <summary>
        /// Name of theme
        /// </summary>
        public String ThemeName { get; set; }
    }
}
