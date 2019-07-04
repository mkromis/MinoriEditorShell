using System.ComponentModel.Composition;
using MinoriEditorStudio.Framework.Menus;
using MinoriEditorStudio.Modules.Settings.Commands;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition OpenSettingsMenuItem = new CommandMenuItemDefinition<OpenSettingsCommandDefinition>(
            MainMenu.MenuDefinitions.ToolsOptionsMenuGroup, 0);
    }
}
