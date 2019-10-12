using System;

namespace MinoriEditorShell.Commands
{
    [CommandDefinition]
    public class SwitchToDocumentCommandListDefinition : CommandListDefinition
    {
        public const String CommandName = "Window.SwitchToDocument";
        public override String Name => CommandName;
    }
}
