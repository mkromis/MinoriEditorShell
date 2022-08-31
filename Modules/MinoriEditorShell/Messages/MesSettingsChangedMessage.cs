using MvvmCross.Plugin.Messenger;
using System;

namespace MinoriEditorShell.Messages
{
    /// <summary>
    /// Message for any changes
    /// </summary>
    public class MesSettingsChangedMessage : MvxMessage
    {
        /// <summary>
        /// Constructor for messages among objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public MesSettingsChangedMessage(object sender, string name, object value) : base(sender)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Name of settings that changed
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// New value
        /// </summary>
        public object Value { get; }
    }
}