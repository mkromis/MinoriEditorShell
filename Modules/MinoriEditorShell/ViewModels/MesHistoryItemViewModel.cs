using MinoriEditorShell.Services;
using MvvmCross.ViewModels;
using System;

namespace MinoriEditorShell.ViewModels
{
    public class MesHistoryItemViewModel : MvxNotifyPropertyChanged
    {
        public IMesUndoableAction Action { get; }

        private readonly String _name;
        public String Name => _name ?? Action.Name;

        private MesHistoryItemType _itemType;
        public MesHistoryItemType ItemType
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

        public MesHistoryItemViewModel(IMesUndoableAction action)
        {
            Action = action;
        }

        public MesHistoryItemViewModel(String name)
        {
            _name = name;
        }
    }
}
