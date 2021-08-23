using MvvmCross.ViewModels;
using System;
using System.Windows;

namespace MinoriEditorShell.Platforms.Avalonia.ViewModels
{
    public class MesStatusBarItemViewModel : MvxNotifyPropertyChanged
    {
        private Int32 _index;
        private String _message;

        public Int32 Index
        {
            get => _index;
            internal set => SetProperty(ref _index, value);
        }

        public String Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        // public GridLength Width { get; }

        // public MesStatusBarItemViewModel(String message, GridLength width)
        // {
        //     _message = message;
        //     Width = width;
        // }
    }
}