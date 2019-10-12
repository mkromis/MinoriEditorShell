namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public class ExcludeMenuItemGroupDefinition
    {
        private readonly MenuItemGroupDefinition _menuItemGroupDefinitionToExclude;
        public MenuItemGroupDefinition MenuItemGroupDefinitionToExclude 
        {
            get { return _menuItemGroupDefinitionToExclude; }
        }

        public ExcludeMenuItemGroupDefinition(MenuItemGroupDefinition menuItemGroupDefinition)
        {
            _menuItemGroupDefinitionToExclude = menuItemGroupDefinition;
        }
    }
}
