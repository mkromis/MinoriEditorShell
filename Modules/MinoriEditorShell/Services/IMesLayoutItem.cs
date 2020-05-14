using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Windows.Input;

namespace MinoriEditorShell.Services
{

    public interface IMesLayoutItem : IMvxViewModel
    {
        Guid Id { get; }
        String ContentId { get; }
        ICommand CloseCommand { get; }
        String DisplayName { get; }
        Uri IconSource { get; }
        bool IsSelected { get; set; }
        bool ShouldReopenOnStart { get; }
        void LoadState(BinaryReader reader);
        void SaveState(BinaryWriter writer);
    }
}
