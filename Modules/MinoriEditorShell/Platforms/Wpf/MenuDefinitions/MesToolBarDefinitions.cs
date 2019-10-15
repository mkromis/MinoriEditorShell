using System.ComponentModel.Composition;
using MinoriEditorShell.Platforms.Wpf.ToolBars;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    internal static class MesToolBarDefinitions
    {
        [Export]
        public static MesToolBarDefinition StandardToolBar = new MesToolBarDefinition(0, Resources.ToolBarStandard);
    }
}
