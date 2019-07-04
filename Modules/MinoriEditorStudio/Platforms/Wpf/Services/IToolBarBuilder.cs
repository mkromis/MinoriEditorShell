using MinoriEditorStudio.Platforms.Wpf.ToolBars;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    public interface IToolBarBuilder
    {
        void BuildToolBars(IToolBars result);
        void BuildToolBar(ToolBarDefinition toolBarDefinition, IToolBar result);
    }
}
