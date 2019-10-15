namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public class MesExcludeMenuItemDefinition
    {
        private readonly MesMenuItemDefinition _menuItemDefinitionToExclude;
        public MesMenuItemDefinition MenuItemDefinitionToExclude 
        { 
            get { return _menuItemDefinitionToExclude; } 
        }

        public MesExcludeMenuItemDefinition(MesMenuItemDefinition menuItemDefinition)
        {
            _menuItemDefinitionToExclude = menuItemDefinition;
        }
    }
}
