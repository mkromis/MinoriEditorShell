using MinoriEditorStudio.Properties;
using System;

namespace MinoriEditorStudio.Commands
{
    [CommandDefinition]
    public class ViewToolboxCommandDefinition : CommandDefinition
    {
        public const String CommandName = "View.Toolbox";
        public override String Name => CommandName;
        public override String Text => Resources.ViewToolboxCommandText;
        public override String ToolTip => Resources.ViewToolboxCommandToolTip;
    }
}
