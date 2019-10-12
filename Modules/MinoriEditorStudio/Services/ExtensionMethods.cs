using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MinoriEditorShell.Services
{
    public static class ExtensionMethods
	{
        public static String GetExecutingAssemblyName() => 
            Assembly.GetExecutingAssembly().GetName().FullName;

        public static String GetPropertyName<TProperty>(Expression<Func<TProperty>> property) => 
            property.Name;

        public static String GetPropertyName<TTarget, TProperty>(Expression<Func<TTarget, TProperty>> property) => 
            property.Body.NodeType.ToString();
    }
}
