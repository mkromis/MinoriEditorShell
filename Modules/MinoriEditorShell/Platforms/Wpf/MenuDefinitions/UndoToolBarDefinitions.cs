using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.ToolBars;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class UndoToolBarDefinitions
    {
        [Export]
        public static ToolBarItemGroupDefinition StandardUndoRedoToolBarGroup = new ToolBarItemGroupDefinition(
            ToolBarDefinitions.StandardToolBar, 10);

        [Export]
        public static ToolBarItemDefinition UndoToolBarItem = new CommandToolBarItemDefinition<UndoCommandDefinition>(
            StandardUndoRedoToolBarGroup, 0);

        [Export]
        public static ToolBarItemDefinition RedoToolBarItem = new CommandToolBarItemDefinition<RedoCommandDefinition>(
            StandardUndoRedoToolBarGroup, 1);
    }
}
