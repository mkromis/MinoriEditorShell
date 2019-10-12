using MinoriEditorShell.Platforms.Wpf.ToolBars;
using MinoriEditorShell.Services;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    public interface IToolBarBuilder
    {
        void BuildToolBars(IToolBars result);
        void BuildToolBar(ToolBarDefinition toolBarDefinition, IToolBar result);
    }
}
