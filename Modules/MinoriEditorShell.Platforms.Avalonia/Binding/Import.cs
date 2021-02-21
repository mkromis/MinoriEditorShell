using System.Reflection;
using MvvmCross.Base;
using MvvmCross.Converters;
using MvvmCross.IoC;
using MvvmCross.Binding.Combiners;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public class Import
    {
        static Import()
        {
            MesDesignTimeChecker.Check();
        }

        private object _from;

        public object From
        {
            get { return _from; }
            set
            {
                if (_from == value)
                    return;

                _from = value;
                if (_from != null)
                {
                    RegisterAssembly(_from.GetType().Assembly);
                }
            }
        }

        private static void RegisterAssembly(Assembly assembly)
        {
            if (MvxSingleton<IMvxIoCProvider>.Instance == null)
            {
                MesWindowsAssemblyCache.EnsureInitialized();
                MesWindowsAssemblyCache.Instance?.Assemblies.Add(assembly);
            }
            else
            {
                Mvx.IoCProvider.CallbackWhenRegistered<IMvxValueConverterRegistry>(
                    registry =>
                        {
                            registry.AddOrOverwriteFrom(assembly);
                        });
                Mvx.IoCProvider.CallbackWhenRegistered<IMvxValueCombinerRegistry>(
                    registry =>
                        {
                            registry.AddOrOverwriteFrom(assembly);
                        });
            }
        }
    }
}
