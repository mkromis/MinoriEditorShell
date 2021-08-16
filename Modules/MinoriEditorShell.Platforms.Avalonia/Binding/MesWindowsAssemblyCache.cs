using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Base;
using MvvmCross.Exceptions;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    /// <summary>
    /// Assembly cache setup
    /// </summary>
    public class MesWindowsAssemblyCache
        : MvxSingleton<IMesWindowsAssemblyCache>
          , IMesWindowsAssemblyCache
    {
        /// <summary>
        /// Helper to make sure class is init properly
        /// </summary>
        public static void EnsureInitialized()
        {
            if (Instance != null)
                return;

            MesWindowsAssemblyCache instance = new MesWindowsAssemblyCache();

            if (Instance != instance)
                throw new MvxException("Error initialising MvxWindowsAssemblyCache");
        }

        /// <summary>
        /// Setups assbembly cache
        /// </summary>
        public MesWindowsAssemblyCache()
        {
            Assemblies = new List<Assembly>();
        }

        /// <summary>
        /// Gets assembly cache
        /// </summary>
        public IList<Assembly> Assemblies { get; }
    }
}
