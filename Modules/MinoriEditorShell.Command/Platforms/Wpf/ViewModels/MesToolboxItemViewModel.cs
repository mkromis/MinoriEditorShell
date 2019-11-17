using MinoriEditorShell.Models;
using System;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    public class MesToolboxItemViewModel
    {
        public MesToolboxItem Model { get; }

        public String Name => Model.Name;

        public virtual String Category => Model.Category;

        public virtual Uri IconSource => Model.IconSource;

        public MesToolboxItemViewModel(MesToolboxItem model)
        {
            Model = model;
        }
    }
}
