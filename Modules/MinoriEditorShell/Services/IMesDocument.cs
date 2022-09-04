using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Document interface
    /// </summary>
    public interface IMesDocument : IMesLayoutItem
    {
        /// <summary>
        /// View attached to view model
        /// </summary>
        IMvxView View { get; set; }

        /// <summary>
        /// Determines weather document can be closed.
        /// </summary>
        bool CanClose { get; }
    }
}