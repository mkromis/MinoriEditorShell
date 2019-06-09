using System;

namespace MinoriEditorStudio.Framework.Commands
{
    public abstract class CommandListDefinition : CommandDefinitionBase
    {
        public sealed override string Text => "[NotUsed]";

        public sealed override string ToolTip => "[NotUsed]";

        public sealed override Uri IconSource => null;

        public sealed override bool IsList => true;
    }
}
