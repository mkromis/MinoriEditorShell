using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    public interface IMesThemeManager
    {
        IEnumerable<IMesTheme> Themes { get; }
        IMesTheme CurrentTheme { get; }

        bool SetCurrentTheme(string name);
    }
}