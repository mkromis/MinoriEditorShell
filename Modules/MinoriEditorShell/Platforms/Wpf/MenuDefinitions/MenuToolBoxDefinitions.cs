using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MenuToolBoxDefinitions
    {
        [Export]
        public static MenuItemDefinition ViewToolboxMenuItem = new CommandMenuItemDefinition<ViewToolboxCommandDefinition>(MenuDefinitionsCollection.ViewToolsMenuGroup, 4);
    }
}
