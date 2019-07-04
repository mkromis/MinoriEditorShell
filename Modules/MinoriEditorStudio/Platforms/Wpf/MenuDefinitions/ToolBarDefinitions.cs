using System.ComponentModel.Composition;
using MinoriEditorStudio.Platforms.Wpf.ToolBars;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitionCollection
{
    internal static class ToolBarDefinitions
    {
        [Export]
        public static ToolBarDefinition StandardToolBar = new ToolBarDefinition(0, Resources.ToolBarStandard);
    }
}
