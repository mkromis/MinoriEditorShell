using MvvmCross.Views;
using System;

namespace MinoriEditorStudio.Services
{
    public interface ITool : ILayoutItem
	{
		PaneLocation PreferredLocation { get; }
        Double PreferredWidth { get; }
        Double PreferredHeight { get; }
        IMvxView View { get; set; }
        Boolean IsVisible { get; set; }

        Boolean CanClose { get; }
	}
}
