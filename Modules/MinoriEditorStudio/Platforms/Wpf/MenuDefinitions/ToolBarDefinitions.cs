using System.ComponentModel.Composition;
using MinoriEditorStudio.Framework.ToolBars;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Platforms.Wpf.MenuDefinitions
{
    internal static class ToolBarDefinitions
    {
        [Export]
        public static ToolBarDefinition StandardToolBar = new ToolBarDefinition(0, Resources.ToolBarStandard);
    }
}
