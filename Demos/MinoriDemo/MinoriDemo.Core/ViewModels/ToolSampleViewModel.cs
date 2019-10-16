using MinoriEditorShell.Services;

namespace MinoriDemo.Core.ViewModels
{
    class ToolSampleViewModel : MesTool
    {
        public override MesPaneLocation PreferredLocation { get; } = MesPaneLocation.Right;
        
        public ToolSampleViewModel()
        {
            DisplayName = "Tool Test";
        }
    }
}
