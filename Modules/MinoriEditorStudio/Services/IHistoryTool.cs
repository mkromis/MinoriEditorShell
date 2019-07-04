using MinoriEditorStudio.Framework;

namespace MinoriEditorStudio.Services
{
    public interface IHistoryTool : ITool
    {
        IUndoRedoManager UndoRedoManager { get; set; }
    }
}
