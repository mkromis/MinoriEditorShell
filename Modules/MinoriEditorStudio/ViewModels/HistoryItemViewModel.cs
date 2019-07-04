using MinoriEditorStudio.Services;
using MvvmCross.ViewModels;
using System;

namespace MinoriEditorStudio.ViewModels
{
    public class HistoryItemViewModel : MvxNotifyPropertyChanged
    {
        public IUndoableAction Action { get; }

        private readonly String _name;
        public String Name => _name ?? Action.Name;

        private HistoryItemType _itemType;
        public HistoryItemType ItemType
        {
            get => _itemType;
            set
            {
                if (_itemType == value)
                    return;

                _itemType = value;

                RaisePropertyChanged(() => ItemType);
            }
        }

        public HistoryItemViewModel(IUndoableAction action)
        {
            Action = action;
        }

        public HistoryItemViewModel(String name)
        {
            _name = name;
        }
    }
}
