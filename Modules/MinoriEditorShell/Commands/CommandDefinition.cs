using System;

namespace MinoriEditorShell.Commands
{
    public abstract class CommandDefinition : CommandDefinitionBase
    {
        public override Uri IconSource => null;

        public sealed override bool IsList => false;
    }
}
