namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public class MesExcludeToolBarItemDefinition
    {
        private readonly MesToolBarItemDefinition _toolBarItemDefinitionToExclude;
        public MesToolBarItemDefinition ToolBarItemDefinitionToExclude
        {
            get { return _toolBarItemDefinitionToExclude; }
        }

        public MesExcludeToolBarItemDefinition(MesToolBarItemDefinition ToolBarItemDefinition)
        {
            _toolBarItemDefinitionToExclude = ToolBarItemDefinition;
        }
    }
}
