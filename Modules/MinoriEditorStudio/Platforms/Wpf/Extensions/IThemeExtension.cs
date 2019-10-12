using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MinoriEditorShell.Platforms.Wpf.Extensions
{
    public static class IThemeExtension
    {
        /// <summary>
        /// Try to see if Mes.Ribbon is loaded.
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static Boolean HasRibbon(this ITheme theme)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Any(x => x.FullName.Contains("MinoriEditorStudio.Ribbon"));
        }
    }
}
