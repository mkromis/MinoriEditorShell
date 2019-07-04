using System;
using MinoriEditorStudio.Modules.Toolbox.Models;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    public class ToolboxItemViewModel
    {
        private readonly ToolboxItem _model;

        public ToolboxItem Model
        {
            get { return _model; }
        }

        public string Name
        {
            get { return _model.Name; }
        }

        public virtual string Category
        {
            get { return _model.Category; }
        }

        public virtual Uri IconSource
        {
            get { return _model.IconSource; }
        }

        public ToolboxItemViewModel(ToolboxItem model)
        {
            _model = model;
        }
    }
}
