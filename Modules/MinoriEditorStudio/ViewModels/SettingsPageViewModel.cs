using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.ViewModels
{
    public class SettingsPageViewModel
    {
        public SettingsPageViewModel()
        {
            Children = new List<SettingsPageViewModel>();
            Editors = new List<ISettingsEditor>();
        }

        public String Name { get; set; }
        public List<ISettingsEditor> Editors { get; private set; }
        public List<SettingsPageViewModel> Children { get; private set; }
    }
}
