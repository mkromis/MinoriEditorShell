using System;
using System.Collections.Generic;

namespace MinoriEditorStudio.Services
{
    public interface IThemeManager
    {
        IEnumerable<ITheme> Themes { get; }
        ITheme CurrentTheme { get; }

        Boolean SetCurrentTheme(String name);
    }
}
