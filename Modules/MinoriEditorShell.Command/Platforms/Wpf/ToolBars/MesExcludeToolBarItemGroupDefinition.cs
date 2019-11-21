namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public class MesExcludeToolBarItemGroupDefinition
    {
        private readonly MesToolBarItemGroupDefinition _toolBarItemGroupDefinitionToExclude;
        public MesToolBarItemGroupDefinition ToolBarItemGroupDefinitionToExclude
        {
            get { return _toolBarItemGroupDefinitionToExclude; }
        }

        public MesExcludeToolBarItemGroupDefinition(MesToolBarItemGroupDefinition toolBarItemGroupDefinition)
        {
            _toolBarItemGroupDefinitionToExclude = toolBarItemGroupDefinition;
        }
    }
}
