using MinoriEditorStudio.Models;
using MinoriEditorStudio.Platforms.Wpf.ViewModels;

namespace MinoriEditorStudio.Platforms.Wpf.Design
{
    public class DesignTimeToolboxViewModel : ToolboxViewModel
    {
        public DesignTimeToolboxViewModel()
            : base(null, null)
        {
            Items.Add(new ToolboxItemViewModel(new ToolboxItem { Name = "Foo", Category = "General" }));
            Items.Add(new ToolboxItemViewModel(new ToolboxItem { Name = "Bar", Category = "General" }));
            Items.Add(new ToolboxItemViewModel(new ToolboxItem { Name = "Baz", Category = "Misc" }));
        }
    }
}
