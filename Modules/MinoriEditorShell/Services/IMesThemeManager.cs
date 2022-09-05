using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Manager of themes, used by IoC
    /// </summary>
    public interface IMesThemeManager
    {
        /// <summary>
        /// Theme names
        /// </summary>
        IEnumerable<IMesTheme> Themes { get; }
        /// <summary>
        /// Selected theme
        /// </summary>
        IMesTheme CurrentTheme { get; }
        /// <summary>
        /// Set theme by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool SetCurrentTheme(string name);
    }
}