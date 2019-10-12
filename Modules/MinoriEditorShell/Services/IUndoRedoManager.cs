using MvvmCross.ViewModels;
using System;
using System.ComponentModel;

namespace MinoriEditorShell.Services
{
    public interface IUndoRedoManager : INotifyPropertyChanged
    {
        MvxObservableCollection<IUndoableAction> ActionStack { get; }
        IUndoableAction CurrentAction { get; }
        int UndoActionCount { get; }
        int RedoActionCount { get; }

        event EventHandler BatchBegin;
        event EventHandler BatchEnd;

        int? UndoCountLimit { get; set; }

        void ExecuteAction(IUndoableAction action);

        bool CanUndo { get; }
        void Undo(int actionCount);
        void UndoTo(IUndoableAction action);
        void UndoAll();

        bool CanRedo { get; }
        void Redo(int actionCount);
        void RedoTo(IUndoableAction action);
    }
}
