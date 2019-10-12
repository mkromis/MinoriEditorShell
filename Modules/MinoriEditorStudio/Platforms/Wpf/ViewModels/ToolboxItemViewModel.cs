using MinoriEditorShell.Models;
using System;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    public class ToolboxItemViewModel
    {
        public ToolboxItem Model { get; }

        public String Name => Model.Name;

        public virtual String Category => Model.Category;

        public virtual Uri IconSource => Model.IconSource;

        public ToolboxItemViewModel(ToolboxItem model)
        {
            Model = model;
        }
    }
}
