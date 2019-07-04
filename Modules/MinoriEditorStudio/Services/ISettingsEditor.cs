namespace MinoriEditorStudio.Services
{
    public interface ISettingsEditor
    {
        string SettingsPageName { get; }
        string SettingsPagePath { get; }

        void ApplyChanges();
    }
}
