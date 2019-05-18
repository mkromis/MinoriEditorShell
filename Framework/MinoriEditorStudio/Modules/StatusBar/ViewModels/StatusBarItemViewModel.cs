using MvvmCross.ViewModels;
using System.Windows;

namespace MinoriEditorStudio.Modules.StatusBar.ViewModels
{
    public class StatusBarItemViewModel : MvxNotifyPropertyChanged
    {
        private int _index;
        private string _message;

        public int Index
        {
            get => _index;
            internal set => SetProperty(ref _index, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public GridLength Width { get; }

        public StatusBarItemViewModel(string message, GridLength width)
        {
            _message = message;
            Width = width;
        }
    }
}
