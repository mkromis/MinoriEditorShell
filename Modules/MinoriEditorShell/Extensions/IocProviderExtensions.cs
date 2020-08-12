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
            IOrderedEnumerable<AssemblyName> assemblies = Assembly.GetEntryAssembly()
                .GetReferencedAssemblies()
                .OrderBy(x => x.FullName);

            List<AssemblyName> assyArray = assemblies.ToList();

            // Make sure we get all of Minori, found a condition to where the
            // base assembly was not being loaded resulting in
            // missing general settings item.
            foreach (AssemblyName assy in assemblies.Where(x => x.Name.Contains("Minori")))
            {
                Assembly load = Assembly.ReflectionOnlyLoad(assy.FullName);
                foreach (AssemblyName name in load.GetReferencedAssemblies())
                {
                    if (assyArray.FirstOrDefault(x => x.FullName == name.FullName) == null)
                    {
                        assyArray.Add(name);
                    }
                }
            }

            // add executing assembly
            assyArray.Add(Assembly.GetEntryAssembly().GetName());

            foreach (AssemblyName assy in assyArray)
            {
                Assembly assembly = Assembly.Load(assy);
                Type[] types = assembly.GetTypes();
                IEnumerable<Type> interfaces = types.Where(x => x.GetInterfaces().Contains(typeof(T)));
                IEnumerable<Type> hasFlags = interfaces.Where(x => !x.Attributes.HasFlag(TypeAttributes.Abstract));

                foreach (Type type in hasFlags)
                {
                    results.Add((T)Activator.CreateInstance(type));
                }
            }
            return results;
        }
    }
}