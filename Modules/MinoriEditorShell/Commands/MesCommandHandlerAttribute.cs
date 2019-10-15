using System;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Commands
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class MesCommandHandlerAttribute : ExportAttribute
    {
        public MesCommandHandlerAttribute()
            : base(typeof(ICommandHandler))
        {
            
        }
    }
}
