using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [MesCommandDefinition]
    public class MesOpenSettingsCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "Tools.Options";
        public override String Name => CommandName;
        public override String Text => Resources.ToolsOptionsCommandText;
        public override String ToolTip => Resources.ToolsOptionsCommandToolTip;
    }
}
