using System;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Commands
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandHandlerAttribute : ExportAttribute
    {
        public CommandHandlerAttribute()
            : base(typeof(ICommandHandler))
        {
            
        }
    }
}
