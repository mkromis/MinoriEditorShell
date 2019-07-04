using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Properties;
using MinoriEditorStudio.Services;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    [Export(typeof(IHistoryTool))]
    public class HistoryViewModel : Tool, IHistoryTool
    {
        public override PaneLocation PreferredLocation => PaneLocation.Right;

        private IUndoRedoManager _undoRedoManager;
        public IUndoRedoManager UndoRedoManager
        {
            get { return _undoRedoManager; }
            set
            {
                if (_undoRedoManager == value)
                    return;

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

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex == value)
                    return;

                _selectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);
                UndoOrRedoTo(HistoryItems[value - 1], false);
            }
        }

        public MvxObservableCollection<HistoryItemViewModel> HistoryItems { get; } = new MvxObservableCollection<HistoryItemViewModel>();

        [ImportingConstructor]
        public HistoryViewModel(IManager shell)
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
            HistoryItems.Add(new HistoryItemViewModel(Resources.HistoryInitialState));
            HistoryItems.AddRange(_undoRedoManager.ActionStack.Select(a => new HistoryItemViewModel(a)));
            RefreshItemTypes();
        }

        private void OnUndoRedoManagerActionStackChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var newItems = e.NewItems.Cast<IUndoableAction>().ToArray();
                    for (var i = 0; i < newItems.Length; i++)
                        HistoryItems.Insert(e.NewStartingIndex + i + 1, new HistoryItemViewModel(newItems[i]));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (var i = 0; i < e.OldItems.Count; i++)
                        HistoryItems.RemoveAt(e.OldStartingIndex + 1);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ResetItems();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void OnUndoRedoManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IUndoRedoManager.UndoActionCount):
                    RefreshItemTypes();
                    break;
                default:
                    break;
            }
        }

        private void RefreshItemTypes()
        {
            HistoryItems[0].ItemType = _undoRedoManager.CanUndo ? HistoryItemType.InitialState : HistoryItemType.Current;

            for (var i = 1; i <= _undoRedoManager.ActionStack.Count; i++)
            {
                var delta = _undoRedoManager.UndoActionCount - i;
                if (delta == 0)
                    HistoryItems[i].ItemType = HistoryItemType.Current;
                else
                    HistoryItems[i].ItemType = delta > 0 ? HistoryItemType.Undo : HistoryItemType.Redo;
            }
        }

        public void UndoOrRedoTo(HistoryItemViewModel item, bool setSelectedIndex)
        {
            switch (item.ItemType)
            {
                case HistoryItemType.InitialState:
                    _undoRedoManager.UndoAll();
                    break;
                case HistoryItemType.Undo:
                    _undoRedoManager.UndoTo(item.Action);
                    break;
                case HistoryItemType.Current:
                    break;
                case HistoryItemType.Redo:
                    _undoRedoManager.RedoTo(item.Action);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (setSelectedIndex)
                SelectedIndex = HistoryItems.IndexOf(item) + 1;
        }
    }
}
