using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [CommandDefinition]
    public class ViewHistoryCommandDefinition : CommandDefinition
    {
        public const String CommandName = "View.History";
        public override String Name => CommandName;
        public override String Text => Resources.ViewHistoryCommandText;
        public override String ToolTip => Resources.ViewHistoryCommandToolTip;
    }
}
