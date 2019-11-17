using MinoriEditorShell.Platforms.Wpf.Menus;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public class MesExcludeMenuDefinition
    {
        private readonly MesMenuDefinition _menuDefinitionToExclude;
        public MesMenuDefinition MenuDefinitionToExclude 
        { 
            get { return _menuDefinitionToExclude; } 
        }

        public MesExcludeMenuDefinition(MesMenuDefinition menuDefinition)
        {
            _menuDefinitionToExclude = menuDefinition;
        }
    }
}
