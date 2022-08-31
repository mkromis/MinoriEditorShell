using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MinoriEditorShell.Services
{
    public static class MesExtensionMethods
    {
        public static string GetExecutingAssemblyName() =>
            Assembly.GetExecutingAssembly().GetName().FullName;

        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> property) =>
            property.Name;

        public static string GetPropertyName<TTarget, TProperty>(Expression<Func<TTarget, TProperty>> property) =>
            property.Body.NodeType.ToString();
    }
}