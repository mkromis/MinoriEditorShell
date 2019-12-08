using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.ViewModels
{
    public class MesSettingsPageViewModel
    {
        public MesSettingsPageViewModel()
        {
            Children = new List<MesSettingsPageViewModel>();
            Editors = new List<IMesSettings>();
        }

        public String Name { get; set; }
        public List<IMesSettings> Editors { get; private set; }
        public List<MesSettingsPageViewModel> Children { get; private set; }
    }
}
