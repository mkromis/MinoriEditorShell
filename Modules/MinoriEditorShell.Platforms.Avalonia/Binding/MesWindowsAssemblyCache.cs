using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Base;
using MvvmCross.Exceptions;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public class MesWindowsAssemblyCache
        : MvxSingleton<IMesWindowsAssemblyCache>
          , IMesWindowsAssemblyCache
    {
        public static void EnsureInitialized()
        {
            if (Instance != null)
                return;

            var instance = new MesWindowsAssemblyCache();

            if (Instance != instance)
                throw new MvxException("Error initialising MvxWindowsAssemblyCache");
        }

        public MesWindowsAssemblyCache()
        {
            Assemblies = new List<Assembly>();
        }

        public IList<Assembly> Assemblies { get; }
    }
}
