using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Windows.Input;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Used for base window, tool or other item
    /// </summary>
    public interface IMesLayoutItem : IMvxViewModel
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// Content ID by string
        /// </summary>
        string ContentId { get; }
        /// <summary>
        /// Removes the document from manager
        /// </summary>
        ICommand CloseCommand { get; }
        /// <summary>
        /// Window or tool title
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Icon by Uri.
        /// </summary>
        Uri IconSource { get; }
        /// <summary>
        /// Weather window is selected
        /// </summary>
        bool IsSelected { get; set; }
        /// <summary>
        /// Determines if item should be open on launch
        /// </summary>
        bool ShouldReopenOnStart { get; }
        /// <summary>
        /// Load state to file
        /// </summary>
        /// <param name="reader"></param>
        void LoadState(BinaryReader reader);
        /// <summary>
        /// Save state to file
        /// </summary>
        /// <param name="writer"></param>
        void SaveState(BinaryWriter writer);
    }
}