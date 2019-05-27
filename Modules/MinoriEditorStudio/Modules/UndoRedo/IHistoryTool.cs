using MinoriEditorStudio.Framework;

namespace MinoriEditorStudio.Modules.UndoRedo
{
    public interface IHistoryTool : ITool
    {
        IUndoRedoManager UndoRedoManager { get; set; }
    }
}
