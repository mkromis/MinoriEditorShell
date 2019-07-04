using MinoriEditorStudio.Platforms.Wpf.Commands;
using MinoriEditorStudio.Platforms.Wpf.ToolBars;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    public static class ToolBarDefinitions
    {
        [Export]
        public static ToolBarItemGroupDefinition StandardUndoRedoToolBarGroup = new ToolBarItemGroupDefinition(
            ToolBars.ToolBarDefinitions.StandardToolBar, 10);

        [Export]
        public static ToolBarItemDefinition UndoToolBarItem = new CommandToolBarItemDefinition<UndoCommandDefinition>(
            StandardUndoRedoToolBarGroup, 0);

        [Export]
        public static ToolBarItemDefinition RedoToolBarItem = new CommandToolBarItemDefinition<RedoCommandDefinition>(
            StandardUndoRedoToolBarGroup, 1);
    }
}
