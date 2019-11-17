namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public class MesExcludeToolBarDefinition
    {
        private readonly MesToolBarDefinition _toolBarDefinitionToExclude;
        public MesToolBarDefinition ToolBarDefinitionToExclude
        {
            get { return _toolBarDefinitionToExclude; }
        }

        public MesExcludeToolBarDefinition(MesToolBarDefinition toolBarDefinition)
        {
            _toolBarDefinitionToExclude = toolBarDefinition;
        }
    }
}
