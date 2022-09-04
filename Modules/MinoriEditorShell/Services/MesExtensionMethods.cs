using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MinoriEditorShell.Services
{
#warning Not Implemented
#if false
    public static class MesExtensionMethods
    {
        public static string GetExecutingAssemblyName() =>
            

        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> property) =>
            property.Name;

        public static string GetPropertyName<TTarget, TProperty>(Expression<Func<TTarget, TProperty>> property) =>
            property.Body.NodeType.ToString();
    }
#endif
}