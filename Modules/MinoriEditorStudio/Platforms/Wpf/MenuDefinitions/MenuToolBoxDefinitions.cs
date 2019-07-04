using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MenuToolBoxDefinitions
    {
        [Export]
        public static MenuItemDefinition ViewToolboxMenuItem = new CommandMenuItemDefinition<ViewToolboxCommandDefinition>(MenuDefinitionsCollection.ViewToolsMenuGroup, 4);
    }
}
