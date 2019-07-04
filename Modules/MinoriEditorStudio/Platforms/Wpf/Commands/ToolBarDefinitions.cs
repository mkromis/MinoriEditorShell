using MinoriEditorStudio.Platforms.Wpf.ToolBars;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    public static class ToolBarDefinitions
    {
        [Export]
        public static ToolBarItemGroupDefinition StandardOpenSaveToolBarGroup = new ToolBarItemGroupDefinition(
            ToolBarDefinition.StandardToolBar, 8);

        [Export]
        public static ToolBarItemDefinition OpenFileToolBarItem = new CommandToolBarItemDefinition<OpenFileCommandDefinition>(
            StandardOpenSaveToolBarGroup, 0);

        [Export]
        public static ToolBarItemDefinition SaveFileToolBarItem = new CommandToolBarItemDefinition<SaveFileCommandDefinition>(
            StandardOpenSaveToolBarGroup, 2);

        [Export]
        public static ToolBarItemDefinition SaveAllFilesToolBarItem = new CommandToolBarItemDefinition<SaveAllFilesCommandDefinition>(
            StandardOpenSaveToolBarGroup, 4);
    }
}
