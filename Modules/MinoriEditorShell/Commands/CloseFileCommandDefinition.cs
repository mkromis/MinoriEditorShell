using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [CommandDefinition]
    public class CloseFileCommandDefinition : CommandDefinition
    {
        public const String CommandName = "File.CloseFile";

        public override String Name => CommandName;

        public override String Text => Resources.FileCloseCommandText;

        public override string ToolTip => Resources.FileCloseCommandToolTip;
    }
}
