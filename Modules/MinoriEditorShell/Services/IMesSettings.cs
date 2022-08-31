using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Settings view model outline
    /// </summary>
    public interface IMesSettings : IMvxViewModel
    {
        /// <summary>
        /// Page name
        /// </summary>
        string SettingsPageName { get; }

        /// <summary>
        /// Path to the page
        /// </summary>
        string SettingsPagePath { get; }

        /// <summary>
        /// View page, automatically set by the presenter
        /// </summary>
        IMvxView View { get; set; }

        /// <summary>
        /// Changes that are applied.
        /// </summary>
        void ApplyChanges();
    }
}