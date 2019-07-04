using MinoriEditorStudio.Framework.Commands;

namespace MinoriEditorStudio.Commands
{
    [CommandDefinition]
    public class SwitchToDocumentCommandListDefinition : CommandListDefinition
    {
        public const string CommandName = "Window.SwitchToDocument";

        public override string Name
        {
            get { return CommandName; }
        }
    }
}
