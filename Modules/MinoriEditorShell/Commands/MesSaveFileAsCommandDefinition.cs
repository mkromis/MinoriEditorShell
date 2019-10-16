using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [MesCommandDefinition]
    public class MesSaveFileAsCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "File.SaveFileAs";
        public override String Name => CommandName;
        public override String Text => Resources.FileSaveAsCommandText;
        public override String ToolTip => Resources.FileSaveAsCommandToolTip;
    }
}
