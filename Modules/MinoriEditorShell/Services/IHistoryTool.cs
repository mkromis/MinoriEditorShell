namespace MinoriEditorShell.Services
{
    public interface IHistoryTool : ITool
    {
        IUndoRedoManager UndoRedoManager { get; set; }
    }
}
