namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public class MesToolBarDefinition
    {
        public int SortOrder { get; }

        public string Name { get; }

        public MesToolBarDefinition(int sortOrder, string name)
        {
            SortOrder = sortOrder;
            Name = name;
        }
    }
}
