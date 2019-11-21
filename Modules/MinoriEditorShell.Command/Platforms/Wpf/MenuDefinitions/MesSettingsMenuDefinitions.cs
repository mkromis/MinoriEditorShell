using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MesMenuDefinitions
    {
        [Export]
        public static MesMenuItemDefinition OpenSettingsMenuItem = new MesCommandMenuItemDefinition<MesOpenSettingsCommandDefinition>(
            MesMenuDefinitionsCollection.ToolsOptionsMenuGroup, 0);
    }
}
