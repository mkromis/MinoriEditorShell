using System;

namespace MinoriEditorShell.Commands
{
    public abstract class MesCommandListDefinition : MesCommandDefinitionBase
    {
        public sealed override string Text => "[NotUsed]";

        public sealed override string ToolTip => "[NotUsed]";

        public sealed override Uri IconSource => null;

        public sealed override bool IsList => true;
    }
}
