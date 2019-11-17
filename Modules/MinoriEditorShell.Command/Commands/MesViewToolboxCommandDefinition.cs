using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [MesCommandDefinition]
    public class MesViewToolboxCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "View.Toolbox";
        public override String Name => CommandName;
        public override String Text => Resources.ViewToolboxCommandText;
        public override String ToolTip => Resources.ViewToolboxCommandToolTip;
    }
}
