using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Settings manger, helper for settings
    /// </summary>
    public interface IMesSettingsManager : IMvxViewModel
    {
        /// <summary>
        /// Helper to show window
        /// </summary>
        IMvxCommand ShowCommand { get; }
    }
}