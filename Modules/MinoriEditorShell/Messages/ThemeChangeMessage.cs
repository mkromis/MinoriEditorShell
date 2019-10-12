using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorShell.Messages
{
    public class ThemeChangeMessage : MvxMessage
    {
        public ThemeChangeMessage(Object sender, String themeName) : base(sender)
        {
            ThemeName = themeName;
        }
        public String ThemeName { get; set; }
    }
}
