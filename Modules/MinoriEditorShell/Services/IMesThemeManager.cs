using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    public interface IMesThemeManager
    {
        IEnumerable<IMesTheme> Themes { get; }
        IMesTheme CurrentTheme { get; }

        Boolean SetCurrentTheme(String name);
    }
}
