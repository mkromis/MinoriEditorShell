using System.ComponentModel.Composition;
using MinoriEditorShell.Platforms.Wpf.ToolBars;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    internal static class ToolBarDefinitions
    {
        [Export]
        public static ToolBarDefinition StandardToolBar = new ToolBarDefinition(0, Resources.ToolBarStandard);
    }
}
