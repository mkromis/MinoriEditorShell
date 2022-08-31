using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
    public interface IMesTool : IMesLayoutItem
    {
        MesPaneLocation PreferredLocation { get; }
        double PreferredWidth { get; }
        double PreferredHeight { get; }
        IMvxView View { get; set; }
        bool IsVisible { get; set; }

        bool CanClose { get; }
    }
}