using MinoriEditorStudio.Services;

namespace MinoriDemo.Core.ViewModels
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
