using MinoriEditorStudio.Properties;
using System;

namespace MinoriEditorStudio.Commands
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
