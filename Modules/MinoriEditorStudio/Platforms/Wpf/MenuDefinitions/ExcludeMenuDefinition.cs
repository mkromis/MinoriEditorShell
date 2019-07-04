using MinoriEditorStudio.Platforms.Wpf.Menus;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    public class ExcludeMenuDefinition
    {
        private readonly MenuDefinition _menuDefinitionToExclude;
        public MenuDefinition MenuDefinitionToExclude 
        { 
            get { return _menuDefinitionToExclude; } 
        }

        public ExcludeMenuDefinition(MenuDefinition menuDefinition)
        {
            _menuDefinitionToExclude = menuDefinition;
        }
    }
}
