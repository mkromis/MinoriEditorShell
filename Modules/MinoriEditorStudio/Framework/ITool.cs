using MinoriEditorStudio.Framework.Services;

namespace MinoriEditorStudio.Framework
{
	public interface ITool : ILayoutItem
	{
		PaneLocation PreferredLocation { get; }
        double PreferredWidth { get; }
        double PreferredHeight { get; }

		bool IsVisible { get; set; }
	}
}
