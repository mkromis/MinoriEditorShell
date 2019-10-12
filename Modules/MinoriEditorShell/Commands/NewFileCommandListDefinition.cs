using System;

namespace MinoriEditorShell.Commands
{
    [CommandDefinition]
    public class NewFileCommandListDefinition : CommandListDefinition
    {
        public const String CommandName = "File.NewFile";

        public override String Name => CommandName;
    }
}
