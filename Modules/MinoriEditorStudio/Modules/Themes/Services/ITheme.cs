using System;
using System.Collections.Generic;

namespace MinoriEditorStudio.Modules.Themes.Services
{
    public interface ITheme
    {
        /// <summary>
        /// Name of the theme, used in menus and settings.
        /// </summary>
        String Name { get; }
        
        /// <summary>
        /// Resources to be added to Application.Resources.
        /// </summary>
        IEnumerable<Uri> ApplicationResources { get; }

        /// <summary>
        /// Resources to be added to the main window's Window.Resources collection.
        /// </summary>
        IEnumerable<Uri> MainWindowResources { get; }
    }
}
