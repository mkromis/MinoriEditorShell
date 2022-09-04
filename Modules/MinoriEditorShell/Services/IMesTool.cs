using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Tool windows
    /// </summary>
    public interface IMesTool : IMesLayoutItem
    {
        /// <summary>
        /// Preferred location
        /// </summary>
        MesPaneLocation PreferredLocation { get; }
        /// <summary>
        /// Ideal width
        /// </summary>
        double PreferredWidth { get; }
        /// <summary>
        /// Ideal height
        /// </summary>
        double PreferredHeight { get; }
        /// <summary>
        /// Associated view
        /// </summary>
        IMvxView View { get; set; }
        /// <summary>
        /// If Visible
        /// </summary>
        bool IsVisible { get; set; }
        /// <summary>
        /// Can the view be closed.
        /// </summary>
        bool CanClose { get; }
    }
}