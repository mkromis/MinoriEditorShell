using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    public interface IThemeManager
    {
        IEnumerable<ITheme> Themes { get; }
        ITheme CurrentTheme { get; }

        Boolean SetCurrentTheme(String name);
    }
}
