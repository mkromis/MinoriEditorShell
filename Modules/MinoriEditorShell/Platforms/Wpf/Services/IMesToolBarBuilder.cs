using MinoriEditorShell.Platforms.Wpf.ToolBars;
using MinoriEditorShell.Services;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    public interface IMesToolBarBuilder
    {
        void BuildToolBars(IMesToolBars result);
        void BuildToolBar(MesToolBarDefinition toolBarDefinition, IMesToolBar result);
    }
}
