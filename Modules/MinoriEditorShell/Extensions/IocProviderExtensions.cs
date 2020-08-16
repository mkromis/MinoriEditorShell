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

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName);
            foreach (Assembly assembly in assemblies)
            {
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