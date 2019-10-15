using System;

namespace MinoriEditorShell.Commands
{
    [MesCommandDefinition]
    public class MesSwitchToDocumentCommandListDefinition : MesCommandListDefinition
    {
        public const String CommandName = "Window.SwitchToDocument";
        public override String Name => CommandName;
    }
}
