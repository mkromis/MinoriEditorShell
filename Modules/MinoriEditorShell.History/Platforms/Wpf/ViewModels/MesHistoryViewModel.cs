using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using MinoriEditorShell.Properties;
using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    [Export(typeof(IMesHistoryTool))]
    public class MesHistoryViewModel : MesTool, IMesHistoryTool
    {
        public override MesPaneLocation PreferredLocation => MesPaneLocation.Right;

        private IMesUndoRedoManager _undoRedoManager;
        public IMesUndoRedoManager UndoRedoManager
        {
            get => _undoRedoManager;
            set
            {
                if (_undoRedoManager == value)
                {
                    return;
                }

                if (_undoRedoManager != null)
                {
                    _undoRedoManager.ActionStack.CollectionChanged -= OnUndoRedoManagerActionStackChanged;
                    _undoRedoManager.PropertyChanged -= OnUndoRedoManagerPropertyChanged;
                }

                _undoRedoManager = value;

                if (_undoRedoManager != null)
                {
                    _undoRedoManager.ActionStack.CollectionChanged += OnUndoRedoManagerActionStackChanged;
                    _undoRedoManager.PropertyChanged += OnUndoRedoManagerPropertyChanged;

                    ResetItems();
                }
            }
        }

        private Int32 _selectedIndex;
        public Int32 SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex == value)
                {
                    return;
                }

                _selectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);
                UndoOrRedoTo(HistoryItems[value - 1], false);
            }
        }

        public MvxObservableCollection<MesHistoryItemViewModel> HistoryItems { get; } = new MvxObservableCollection<MesHistoryItemViewModel>();

        [ImportingConstructor]
        public MesHistoryViewModel(IMesManager shell)
        {
            #warning HistoryViewModel ctor
#if false
            DisplayName = Resources.HistoryDisplayName;

            if (shell == null)
                return;

            shell.ActiveDocumentChanged += (sender, e) =>
            {
                UndoRedoManager = (shell.ActiveItem != null) ? shell.ActiveItem.UndoRedoManager : null;
            };
            if (shell.ActiveItem != null)
                UndoRedoManager = shell.ActiveItem.UndoRedoManager;
#endif
        }

        private void ResetItems()
        {
            HistoryItems.Clear();
            HistoryItems.Add(new MesHistoryItemViewModel(Resources.HistoryInitialState));
            HistoryItems.AddRange(_undoRedoManager.ActionStack.Select(a => new MesHistoryItemViewModel(a)));
            RefreshItemTypes();
        }

        private void OnUndoRedoManagerActionStackChanged(Object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    IMesUndoableAction[] newItems = e.NewItems.Cast<IMesUndoableAction>().ToArray();
                    for (Int32 i = 0; i < newItems.Length; i++)
                    {
                        HistoryItems.Insert(e.NewStartingIndex + i + 1, new MesHistoryItemViewModel(newItems[i]));
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (Int32 i = 0; i < e.OldItems.Count; i++)
                    {
                        HistoryItems.RemoveAt(e.OldStartingIndex + 1);
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    ResetItems();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void OnUndoRedoManagerPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IMesUndoRedoManager.UndoActionCount):
                    RefreshItemTypes();
                    break;
                default:
                    break;
            }
        }

        private void RefreshItemTypes()
        {
            HistoryItems[0].ItemType = _undoRedoManager.CanUndo ? MesHistoryItemType.InitialState : MesHistoryItemType.Current;

            for (Int32 i = 1; i <= _undoRedoManager.ActionStack.Count; i++)
            {
                Int32 delta = _undoRedoManager.UndoActionCount - i;
                HistoryItems[i].ItemType = delta == 0 ? 
                    MesHistoryItemType.Current : delta > 0 ? MesHistoryItemType.Undo : MesHistoryItemType.Redo;
            }
        }

        public void UndoOrRedoTo(MesHistoryItemViewModel item, Boolean setSelectedIndex)
        {
            switch (item.ItemType)
            {
                case MesHistoryItemType.InitialState:
                    _undoRedoManager.UndoAll();
                    break;
                case MesHistoryItemType.Undo:
                    _undoRedoManager.UndoTo(item.Action);
                    break;
                case MesHistoryItemType.Current:
                    break;
                case MesHistoryItemType.Redo:
                    _undoRedoManager.RedoTo(item.Action);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (setSelectedIndex)
            {
                SelectedIndex = HistoryItems.IndexOf(item) + 1;
            }
        }
    }
}
