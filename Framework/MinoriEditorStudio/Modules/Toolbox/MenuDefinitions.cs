using System.ComponentModel.Composition;
using MinoriEditorStudio.Framework.Menus;
using MinoriEditorStudio.Modules.Toolbox.Commands;

namespace MinoriEditorStudio.Modules.Toolbox
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition ViewToolboxMenuItem = new CommandMenuItemDefinition<ViewToolboxCommandDefinition>(
            MainMenu.MenuDefinitions.ViewToolsMenuGroup, 4);
    }
}
