using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition OpenSettingsMenuItem = new CommandMenuItemDefinition<OpenSettingsCommandDefinition>(
            MenuDefinitionsCollection.ToolsOptionsMenuGroup, 0);
    }
}
