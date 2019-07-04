using System.Collections.Generic;

namespace MinoriEditorStudio.ViewModels
{
    public class SettingsPageViewModel
    {
        public SettingsPageViewModel()
        {
            Children = new List<SettingsPageViewModel>();
            Editors = new List<ISettingsEditor>();
        }

        public string Name { get; set; }
        public List<ISettingsEditor> Editors { get; private set; }
        public List<SettingsPageViewModel> Children { get; private set; }
    }
}
