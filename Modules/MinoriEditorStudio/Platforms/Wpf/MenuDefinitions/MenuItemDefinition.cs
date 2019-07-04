using MinoriEditorStudio.Platforms.Wpf.Menus;
using System;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    public abstract class MenuItemDefinition : MenuDefinitionBase
    {
        private readonly Int32 _sortOrder;

        public MenuItemGroupDefinition Group { get; }

        public override Int32 SortOrder => _sortOrder;

        protected MenuItemDefinition(MenuItemGroupDefinition group, Int32 sortOrder)
        {
            Group = group;
            _sortOrder = sortOrder;
        }
    }
}
