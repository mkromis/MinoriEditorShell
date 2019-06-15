namespace MinoriEditorStudio.Modules.UndoRedo
{
    public interface IUndoableAction
    {
        string Name { get; }

        void Execute();
        void Undo();
    }
}
