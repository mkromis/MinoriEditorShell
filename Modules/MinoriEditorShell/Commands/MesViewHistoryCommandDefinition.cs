using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [MesCommandDefinition]
    public class MesViewHistoryCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "View.History";
        public override String Name => CommandName;
        public override String Text => Resources.ViewHistoryCommandText;
        public override String ToolTip => Resources.ViewHistoryCommandToolTip;
    }
}
