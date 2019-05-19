using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MinoriEditorStudio.Framework.Services
{
#warning ExtensionMethods
    public static class ExtensionMethods
	{
		public static string GetExecutingAssemblyName()
		{
            throw new NotImplementedException();
			//return Assembly.GetExecutingAssembly().GetAssemblyName();
		}

        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> property)
        {
            throw new NotImplementedException();
            //return property.GetMemberInfo().Name;
        }

        public static string GetPropertyName<TTarget, TProperty>(Expression<Func<TTarget, TProperty>> property)
        {
            throw new NotImplementedException();
            //return property.GetMemberInfo().Name;
        }
	}
}
