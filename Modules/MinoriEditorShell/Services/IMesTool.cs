using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
    public interface IMesTool : IMesLayoutItem
	{
		MesPaneLocation PreferredLocation { get; }
        Double PreferredWidth { get; }
        Double PreferredHeight { get; }
        IMvxView View { get; set; }
        Boolean IsVisible { get; set; }

        Boolean CanClose { get; }
	}
}
