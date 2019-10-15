using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Messages
{
    public class MesSettingsChangedMessage : MvxMessage
    {
        public MesSettingsChangedMessage(Object sender, String name, Object value) : base(sender)
        {
            Name = name;
            Value = value;
        }

        public String Name { get; }
        public Object Value { get; }
    }
}
