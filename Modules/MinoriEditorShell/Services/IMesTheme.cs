using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Theme helper
    /// </summary>
    public interface IMesTheme
    {
        /// <summary>
        /// Name of the theme, used in menus and settings.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Resources to be added to Application.Resources.
        /// </summary>
        IEnumerable<Uri> ApplicationResources { get; }
        /// <summary>
        /// Add url
        /// </summary>
        /// <param name="uri"></param>
        void Add(Uri uri);
        /// <summary>
        ///  Add multiple urls 
        /// </summary>
        /// <param name="uri"></param>
        void AddRange(IEnumerable<Uri> uri);
    }
}