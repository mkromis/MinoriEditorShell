using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.ToolBars;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MesUndoToolBarDefinitions
    {
        [Export]
        public static MesToolBarItemGroupDefinition StandardUndoRedoToolBarGroup = new MesToolBarItemGroupDefinition(
            MesToolBarDefinitions.StandardToolBar, 10);

        [Export]
        public static MesToolBarItemDefinition UndoToolBarItem = new MesCommandToolBarItemDefinition<MesUndoCommandDefinition>(
            StandardUndoRedoToolBarGroup, 0);

        [Export]
        public static MesToolBarItemDefinition RedoToolBarItem = new MesCommandToolBarItemDefinition<MesRedoCommandDefinition>(
            StandardUndoRedoToolBarGroup, 1);
    }
}
