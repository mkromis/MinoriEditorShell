using MvvmCross;
using MvvmCross.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MinoriEditorShell.Extensions
{
    public static class IocProviderExtensions
    {
        public static IEnumerable<T> GetAll<T>(this IMvxIoCProvider provider) where T : class
        {
            // Setup results
            List<T> results = new List<T>();

            // Get all assemblies
            AssemblyName[] assyArray = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            foreach (AssemblyName assy in assyArray)
            {
                Assembly assembly = Assembly.Load(assy);
                Type[] types = assembly.GetTypes();
                foreach(Type type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(T))) {
                        try
                        {
                            results.Add((T)Activator.CreateInstance(type));
                        }
                        catch (MissingMethodException) { }
                    }
                }
            }
            //IEnumerable<Type> result = Application.Current.GetType().Assembly.CreatableTypes();

            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISettingsEditor, MainMenuSettingsViewModel>();

            //return result.Select(x => x.GetType() as T).Where(x => x != null);
            return results;
        }
    }
}
