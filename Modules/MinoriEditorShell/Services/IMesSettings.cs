using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
    public interface IMesSettings : IMvxViewModel
    {
        String SettingsPageName { get; }
        String SettingsPagePath { get; }
        IMvxView View { get; set; }

        void ApplyChanges();
    }
}
