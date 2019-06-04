using MinoriEditorStudio.Modules.UndoRedo;
using MvvmCross.Views;

namespace MinoriEditorStudio.Framework
{
	public interface IDocument : ILayoutItem
	{
        string DisplayName { get; set; }
        IMvxView View { get; }
        IUndoRedoManager UndoRedoManager { get; }
	}
}
