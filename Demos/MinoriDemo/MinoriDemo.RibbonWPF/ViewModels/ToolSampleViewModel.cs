using MinoriEditorStudio.Services;

namespace MinoriDemo.RibbonWPF.ViewModels
{
    class ToolSampleViewModel : Tool
    {
        public override PaneLocation PreferredLocation { get; } = PaneLocation.Right;
        
        public ToolSampleViewModel()
        {
            DisplayName = "Tool Test";
        }
    }
}
