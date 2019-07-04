
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.ViewModels
{
    public class HistoryItemViewModel : MvxNotifyPropertyChanged
    {
        public IUndoableAction Action { get; }

        private readonly string _name;
        public string Name => _name ?? Action.Name;

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

        public HistoryItemViewModel(string name)
        {
            _name = name;
        }
    }
}
