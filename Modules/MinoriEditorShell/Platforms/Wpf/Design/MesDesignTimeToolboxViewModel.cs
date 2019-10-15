using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.ViewModels;

namespace MinoriEditorShell.Platforms.Wpf.Design
{
    public class MesDesignTimeToolboxViewModel : MesToolboxViewModel
    {
        public MesDesignTimeToolboxViewModel()
            : base(null, null)
        {
            Items.Add(new MesToolboxItemViewModel(new MesToolboxItem { Name = "Foo", Category = "General" }));
            Items.Add(new MesToolboxItemViewModel(new MesToolboxItem { Name = "Bar", Category = "General" }));
            Items.Add(new MesToolboxItemViewModel(new MesToolboxItem { Name = "Baz", Category = "Misc" }));
        }
    }
}
