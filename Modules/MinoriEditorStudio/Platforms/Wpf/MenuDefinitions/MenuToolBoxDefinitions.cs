using System.ComponentModel.Composition;
using MinoriEditorStudio.Framework.Menus;
using MinoriEditorStudio.Modules.Toolbox.Commands;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition ViewToolboxMenuItem = new CommandMenuItemDefinition<ViewToolboxCommandDefinition>(
            MainMenu.MenuDefinitions.ViewToolsMenuGroup, 4);
    }
}
