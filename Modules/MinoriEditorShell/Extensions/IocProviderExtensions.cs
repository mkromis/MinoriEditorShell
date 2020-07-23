using MvvmCross.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MinoriEditorShell.Extensions
{
    /// <summary>
    /// Helper classes to IocProvider
    /// </summary>
    public static class IocProviderExtensions
    {
        /// <summary>
        /// Try to instantiate all non-abstract classes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAll<T>(this IMvxIoCProvider _) where T : class
        {
            // Setup results
            List<T> results = new List<T>();

            // Get all assemblies
            List<AssemblyName> assyArray = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList();

            // add executing assembly
            assyArray.Add(Assembly.GetEntryAssembly().GetName());

            foreach (AssemblyName assy in assyArray)
            {
                Assembly assembly = Assembly.Load(assy);
                foreach (Type type in assembly.GetTypes()
                    .Where(x => x.GetInterfaces().Contains(typeof(T)))
                    .Where(x => !x.Attributes.HasFlag(TypeAttributes.Abstract)))
                {
                    results.Add((T)Activator.CreateInstance(type));
                }
            }
            return results;
        }
    }
}