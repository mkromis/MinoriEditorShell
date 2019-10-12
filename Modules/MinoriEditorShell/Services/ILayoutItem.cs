using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Windows.Input;

namespace MinoriEditorShell.Services
{
    public interface ILayoutItem : IMvxViewModel
    {
        Guid Id { get; }
        string ContentId { get; }
        ICommand CloseCommand { get; }
        Uri IconSource { get; }
        bool IsSelected { get; set; }
        bool ShouldReopenOnStart { get; }
        void LoadState(BinaryReader reader);
        void SaveState(BinaryWriter writer);
    }
}
