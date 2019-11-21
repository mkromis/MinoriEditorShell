using MinoriEditorShell.Platforms.Wpf.Menus;
using System;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public class MesMenuItemGroupDefinition
    {
        public MesMenuDefinitionBase Parent { get; }

        public Int32 SortOrder { get; }

        public MesMenuItemGroupDefinition(MesMenuDefinitionBase parent, Int32 sortOrder)
        {
            Parent = parent;
            SortOrder = sortOrder;
        }
    }
}
