using System;
using System.Collections.Generic;

namespace MinoriEditorStudio.Services
{
    public interface IThemeManager
    {
        IThemeList Themes { get; }
        ITheme CurrentTheme { get; }

        Boolean SetCurrentTheme(String name);
    }
}
