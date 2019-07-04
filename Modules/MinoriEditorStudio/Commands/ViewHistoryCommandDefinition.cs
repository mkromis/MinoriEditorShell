using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Commands
{
    [CommandDefinition]
    public class ViewHistoryCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.History";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.ViewHistoryCommandText; }
        }

        public override string ToolTip
        {
            get { return Resources.ViewHistoryCommandToolTip; }
        }
    }
}
