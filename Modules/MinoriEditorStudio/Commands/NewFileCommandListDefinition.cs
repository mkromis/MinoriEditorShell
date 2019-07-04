using System;

namespace MinoriEditorStudio.Commands
{
    [CommandDefinition]
    public class NewFileCommandListDefinition : CommandListDefinition
    {
        public const String CommandName = "File.NewFile";

        public override String Name => CommandName;
    }
}
