using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition OpenSettingsMenuItem = new CommandMenuItemDefinition<OpenSettingsCommandDefinition>(
            MenuDefinitionsCollection.ToolsOptionsMenuGroup, 0);
    }
}
