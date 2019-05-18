using MinoriEditorStudio.Modules.UndoRedo;

namespace MinoriEditorStudio.Framework
{
	public interface IDocument : ILayoutItem
	{
        string DisplayName { get; set; }
        IUndoRedoManager UndoRedoManager { get; }
	}
}
