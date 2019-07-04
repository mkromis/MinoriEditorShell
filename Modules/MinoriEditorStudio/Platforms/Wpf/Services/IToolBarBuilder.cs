using MinoriEditorStudio.Framework.ToolBars;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    public interface IToolBarBuilder
    {
        void BuildToolBars(IToolBars result);
        void BuildToolBar(ToolBarDefinition toolBarDefinition, IToolBar result);
    }
}
