using MvvmCross.ViewModels;
using System;
using System.Windows;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
#warning
#if false
    public class MesStatusBarItemViewModel : MvxNotifyPropertyChanged
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

        public MesStatusBarItemViewModel(string message, GridLength width)
        {
            _message = message;
            Width = width;
        }
    }
#endif
}