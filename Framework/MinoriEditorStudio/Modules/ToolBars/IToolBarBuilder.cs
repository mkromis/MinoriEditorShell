using MinoriEditorStudio.Framework.ToolBars;

namespace MinoriEditorStudio.Modules.ToolBars
{
    public interface IToolBarBuilder
    {
        void BuildToolBars(IToolBars result);
        void BuildToolBar(ToolBarDefinition toolBarDefinition, IToolBar result);
    }
}
