using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MesMenuToolBoxDefinitions
    {
        [Export]
        public static MesMenuItemDefinition ViewToolboxMenuItem = new MesCommandMenuItemDefinition<MesViewToolboxCommandDefinition>(MesMenuDefinitionsCollection.ViewToolsMenuGroup, 4);
    }
}
