using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
	public interface IDocument : ILayoutItem
	{
        String DisplayName { get; }
        IMvxView View { get; set; }
        IUndoRedoManager UndoRedoManager { get; }
        Boolean CanClose { get; }
	}
}
