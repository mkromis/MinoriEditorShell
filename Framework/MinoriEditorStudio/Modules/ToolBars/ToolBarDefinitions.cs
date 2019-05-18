using System.ComponentModel.Composition;
using MinoriEditorStudio.Framework.ToolBars;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Modules.ToolBars
{
    internal static class ToolBarDefinitions
    {
        [Export]
        public static ToolBarDefinition StandardToolBar = new ToolBarDefinition(0, Resources.ToolBarStandard);
    }
}
