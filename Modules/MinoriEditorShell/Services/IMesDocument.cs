using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
	public interface IMesDocument : IMesLayoutItem
	{
        String DisplayName { get; }
        IMvxView View { get; set; }
        IMesUndoRedoManager UndoRedoManager { get; }
        Boolean CanClose { get; }
	}
}
