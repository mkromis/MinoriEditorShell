namespace MinoriEditorShell.Services
{
    public interface IMesHistoryTool : IMesTool
    {
        IMesUndoRedoManager UndoRedoManager { get; set; }
    }
}
