using MinoriEditorShell.Properties;
using System;

namespace MinoriEditorShell.Commands
{
    [MesCommandDefinition]
    public class MesCloseFileCommandDefinition : MesCommandDefinition
    {
        public const String CommandName = "File.CloseFile";

        public override String Name => CommandName;

        public override String Text => Resources.FileCloseCommandText;

        public override string ToolTip => Resources.FileCloseCommandToolTip;
    }
}
