using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorShell.Messages
{
    public class MesThemeChangeMessage : MvxMessage
    {
        public MesThemeChangeMessage(Object sender, String themeName) : base(sender)
        {
            ThemeName = themeName;
        }
        public String ThemeName { get; set; }
    }
}
