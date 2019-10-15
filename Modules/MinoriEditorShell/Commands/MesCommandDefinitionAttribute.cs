using System;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Commands
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class MesCommandDefinitionAttribute : ExportAttribute
    {
        public MesCommandDefinitionAttribute()
            : base(typeof(MesCommandDefinitionBase))
        {
            
        }
    }
}
