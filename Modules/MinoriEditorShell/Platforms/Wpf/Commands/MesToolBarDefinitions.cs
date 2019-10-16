using MinoriEditorShell.Platforms.Wpf.ToolBars;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public static class MesToolBarDefinitions
    {
        [Export]
        public static MesToolBarItemGroupDefinition StandardOpenSaveToolBarGroup = null;
            //new ToolBarItemGroupDefinition(ToolBarDefinition.StandardToolBar, 8);

        [Export]
        public static MesToolBarItemDefinition OpenFileToolBarItem = new MesCommandToolBarItemDefinition<MesOpenFileCommandDefinition>(
            StandardOpenSaveToolBarGroup, 0);

        [Export]
        public static MesToolBarItemDefinition SaveFileToolBarItem = new MesCommandToolBarItemDefinition<MesSaveFileCommandDefinition>(
            StandardOpenSaveToolBarGroup, 2);

        [Export]
        public static MesToolBarItemDefinition SaveAllFilesToolBarItem = new MesCommandToolBarItemDefinition<MesSaveAllFilesCommandDefinition>(
            StandardOpenSaveToolBarGroup, 4);
    }
}
