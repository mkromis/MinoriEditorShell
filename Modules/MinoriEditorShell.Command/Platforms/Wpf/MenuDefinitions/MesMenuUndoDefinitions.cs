using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MesMenuUndoDefinitions
    {
        [Export]
        public static MesMenuItemDefinition EditUndoMenuItem = new MesCommandMenuItemDefinition<MesUndoCommandDefinition>(
            MesMenuDefinitionsCollection.EditUndoRedoMenuGroup, 0);

        [Export]
        public static MesMenuItemDefinition EditRedoMenuItem = new MesCommandMenuItemDefinition<MesRedoCommandDefinition>(
            MesMenuDefinitionsCollection.EditUndoRedoMenuGroup, 1);

        [Export]
        public static MesMenuItemDefinition ViewHistoryMenuItem = new MesCommandMenuItemDefinition<MesViewHistoryCommandDefinition>(
            MesMenuDefinitionsCollection.ViewToolsMenuGroup, 5);
    }
}
