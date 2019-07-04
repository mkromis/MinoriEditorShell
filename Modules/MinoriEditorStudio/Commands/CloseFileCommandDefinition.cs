using MinoriEditorStudio.Properties;
using System;

namespace MinoriEditorStudio.Commands
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
