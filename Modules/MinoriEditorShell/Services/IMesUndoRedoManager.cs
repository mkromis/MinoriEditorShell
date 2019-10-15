using MvvmCross.ViewModels;
using System;
using System.ComponentModel;

namespace MinoriEditorShell.Services
{
    public interface IMesUndoRedoManager : INotifyPropertyChanged
    {
        MvxObservableCollection<IMesUndoableAction> ActionStack { get; }
        IMesUndoableAction CurrentAction { get; }
        int UndoActionCount { get; }
        int RedoActionCount { get; }

        event EventHandler BatchBegin;
        event EventHandler BatchEnd;

        int? UndoCountLimit { get; set; }

        void ExecuteAction(IMesUndoableAction action);

        bool CanUndo { get; }
        void Undo(int actionCount);
        void UndoTo(IMesUndoableAction action);
        void UndoAll();

        bool CanRedo { get; }
        void Redo(int actionCount);
        void RedoTo(IMesUndoableAction action);
    }
}
