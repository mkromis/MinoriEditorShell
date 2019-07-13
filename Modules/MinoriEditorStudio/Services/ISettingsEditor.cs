using System;

namespace MinoriEditorStudio.Services
{
    public interface ISettingsEditor
    {
        String SettingsPageName { get; }
        String SettingsPagePath { get; }

        void ApplyChanges();
    }
}
