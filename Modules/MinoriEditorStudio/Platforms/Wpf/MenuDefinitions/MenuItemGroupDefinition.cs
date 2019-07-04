using MinoriEditorStudio.Platforms.Wpf.Menus;
using System;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    public class MenuItemGroupDefinition
    {
        public MenuDefinitionBase Parent { get; }

        public Int32 SortOrder { get; }

        public MenuItemGroupDefinition(MenuDefinitionBase parent, Int32 sortOrder)
        {
            Parent = parent;
            SortOrder = sortOrder;
        }
    }
}
