namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public class MesToolBarItemGroupDefinition
    {
        public MesToolBarDefinition ToolBar { get; }

        public int SortOrder { get; }

        public MesToolBarItemGroupDefinition(MesToolBarDefinition toolBar, int sortOrder)
        {
            ToolBar = toolBar;
            SortOrder = sortOrder;
        }
    }
}
