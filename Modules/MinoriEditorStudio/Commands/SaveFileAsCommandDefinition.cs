using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [CommandDefinition]
    public class SaveFileAsCommandDefinition : CommandDefinition
    {
        public const String CommandName = "File.SaveFileAs";
        public override String Name => CommandName;
        public override String Text => Resources.FileSaveAsCommandText;
        public override String ToolTip => Resources.FileSaveAsCommandToolTip;
    }
}
