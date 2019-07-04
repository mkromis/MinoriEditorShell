using MinoriEditorStudio.Properties;
using System;

namespace MinoriEditorStudio.Commands
{
    [CommandDefinition]
    public class OpenSettingsCommandDefinition : CommandDefinition
    {
        public const String CommandName = "Tools.Options";
        public override String Name => CommandName;
        public override String Text => Resources.ToolsOptionsCommandText;
        public override String ToolTip => Resources.ToolsOptionsCommandToolTip;
    }
}
