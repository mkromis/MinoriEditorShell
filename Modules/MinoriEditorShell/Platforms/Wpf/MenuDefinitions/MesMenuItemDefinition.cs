using MinoriEditorShell.Platforms.Wpf.Menus;
using System;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public abstract class MesMenuItemDefinition : MesMenuDefinitionBase
    {
        private readonly Int32 _sortOrder;

        public MesMenuItemGroupDefinition Group { get; }

        public override Int32 SortOrder => _sortOrder;

        protected MesMenuItemDefinition(MesMenuItemGroupDefinition group, Int32 sortOrder)
        {
            Group = group;
            _sortOrder = sortOrder;
        }
    }
}
