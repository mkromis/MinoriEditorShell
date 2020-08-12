using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    public interface IMesTheme
    {
        /// <summary>
        /// Name of the theme, used in menus and settings.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Resources to be added to Application.Resources.
        /// </summary>
        IEnumerable<Uri> ApplicationResources { get; }

        void Add(Uri uri);

        void AddRange(IEnumerable<Uri> uri);
    }
}