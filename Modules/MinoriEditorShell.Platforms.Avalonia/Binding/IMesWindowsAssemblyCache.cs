using System.Collections.Generic;
using System.Reflection;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public interface IMesWindowsAssemblyCache
    {
        IList<Assembly> Assemblies { get; }
    }
}
