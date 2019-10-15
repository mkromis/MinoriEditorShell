namespace MinoriEditorShell.Services
{
    public interface IMesUndoableAction
    {
        string Name { get; }

        void Execute();
        void Undo();
    }
}
