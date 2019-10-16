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
            Editors = new List<IMesSettingsEditor>();
        }

        public String Name { get; set; }
        public List<IMesSettingsEditor> Editors { get; private set; }
        public List<MesSettingsPageViewModel> Children { get; private set; }
    }
}
