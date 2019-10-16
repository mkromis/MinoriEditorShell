using System;

namespace MinoriEditorShell.Commands
{
    [MesCommandDefinition]
    public class MesNewFileCommandListDefinition : MesCommandListDefinition
    {
        public const String CommandName = "File.NewFile";

        public override String Name => CommandName;
    }
}
