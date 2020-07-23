using MinoriEditorShell.Services;

namespace MinoriDemo.Core.ViewModels
{
    internal class ToolSampleViewModel : MesTool
    {
        public override MesPaneLocation PreferredLocation { get; } = MesPaneLocation.Right;

        public ToolSampleViewModel()
        {
            DisplayName = "Tool Test";
        }
    }
}