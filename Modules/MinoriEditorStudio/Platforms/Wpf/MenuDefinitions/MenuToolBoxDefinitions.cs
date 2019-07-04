using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition ViewToolboxMenuItem = new CommandMenuItemDefinition<ViewToolboxCommandDefinition>(
            MainMenu.MenuDefinitions.ViewToolsMenuGroup, 4);
    }
}
