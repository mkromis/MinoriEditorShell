using MvvmCross.Views;
using System;

namespace MinoriEditorStudio.Services
{
	public interface IDocument : ILayoutItem
	{
        String DisplayName { get; set; }
        IMvxView View { get; set; }
        IUndoRedoManager UndoRedoManager { get; }
        Boolean CanClose { get; }
	}
}
