using System;

namespace MinoriEditorShell.Commands
{
    public abstract class MesCommandDefinitionBase
    {
        public abstract string Name { get; }
        public abstract string Text { get; }
        public abstract string ToolTip { get; }
        public abstract Uri IconSource { get; }
        public abstract bool IsList { get; }
    }
}
