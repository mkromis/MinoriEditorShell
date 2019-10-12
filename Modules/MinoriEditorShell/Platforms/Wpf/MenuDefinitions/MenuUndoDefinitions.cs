using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MenuUndoDefinitions
    {
        [Export]
        public static MenuItemDefinition EditUndoMenuItem = new CommandMenuItemDefinition<UndoCommandDefinition>(
            MenuDefinitionsCollection.EditUndoRedoMenuGroup, 0);

        [Export]
        public static MenuItemDefinition EditRedoMenuItem = new CommandMenuItemDefinition<RedoCommandDefinition>(
            MenuDefinitionsCollection.EditUndoRedoMenuGroup, 1);

        [Export]
        public static MenuItemDefinition ViewHistoryMenuItem = new CommandMenuItemDefinition<ViewHistoryCommandDefinition>(
            MenuDefinitionsCollection.ViewToolsMenuGroup, 5);
    }
}
