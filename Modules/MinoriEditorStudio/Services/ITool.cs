using MinoriEditorStudio.Framework.Services;
using MvvmCross.Views;

namespace MinoriEditorStudio.Services
{
	public interface ITool : ILayoutItem
	{
		PaneLocation PreferredLocation { get; }
        double PreferredWidth { get; }
        double PreferredHeight { get; }
        IMvxView View { get; set; }
		bool IsVisible { get; set; }

        bool CanClose { get; }
	}
}
