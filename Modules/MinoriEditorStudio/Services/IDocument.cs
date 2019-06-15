using MinoriEditorStudio.Modules.UndoRedo;
using MvvmCross.Views;

namespace MinoriEditorStudio.Framework
{
	public interface IDocument : ILayoutItem
	{
        string DisplayName { get; set; }
        IMvxView View { get; set; }
        IUndoRedoManager UndoRedoManager { get; }

        bool CanClose { get; }
	}
}
