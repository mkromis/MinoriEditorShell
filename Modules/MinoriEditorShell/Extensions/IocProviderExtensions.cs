﻿using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Logging;
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
            List<AssemblyName> assyArray = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList();
            
            // add executing assembly
            assyArray.Add(Assembly.GetEntryAssembly().GetName());

            foreach (AssemblyName assy in assyArray)
            {
                Assembly assembly = Assembly.Load(assy);
                Type[] types = assembly.GetTypes();
                foreach(Type type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(T)) && !type.Attributes.HasFlag(TypeAttributes.Abstract)) {
                        results.Add((T)Activator.CreateInstance(type));
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
