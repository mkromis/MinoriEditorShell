using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Main Window
    /// </summary>
    public interface IMesWindow
    {
        /// <summary>
        /// Same as Title
        /// </summary>
        string DisplayName { get; set; }
        /// <summary>
        /// Title of window
        /// </summary>
        string Title { get; set; }
    }
}