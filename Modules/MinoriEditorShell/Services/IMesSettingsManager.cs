using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Services
{
    public interface IMesSettingsManager : IMvxViewModel
    {
        IMvxCommand ShowCommand { get; }
    }
}