using System;

namespace MinoriEditorShell.Commands
{
    public abstract class MesCommandDefinition : MesCommandDefinitionBase
    {
        public override Uri IconSource => null;

        public sealed override bool IsList => false;
    }
}
