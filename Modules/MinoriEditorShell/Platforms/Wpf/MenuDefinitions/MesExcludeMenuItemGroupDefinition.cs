namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public class MesExcludeMenuItemGroupDefinition
    {
        private readonly MesMenuItemGroupDefinition _menuItemGroupDefinitionToExclude;
        public MesMenuItemGroupDefinition MenuItemGroupDefinitionToExclude 
        {
            get { return _menuItemGroupDefinitionToExclude; }
        }

        public MesExcludeMenuItemGroupDefinition(MesMenuItemGroupDefinition menuItemGroupDefinition)
        {
            _menuItemGroupDefinitionToExclude = menuItemGroupDefinition;
        }
    }
}
