using MvvmCross;
using MvvmCross.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MinoriEditorStudio.Platforms.Wpf.Extensions
{
    public static class IocProviderExtensions
    {
        public static IEnumerable<T> GetAll<T>(this IMvxIoCProvider provider) where T : class
        {
            IEnumerable<MvxTypeExtensions.ServiceTypeAndImplementationTypePair> result = Application.Current.GetType().Assembly.CreatableTypes().AsInterfaces(typeof(T));
            return result.Select(x => x.GetType() as T).Where(x => x != null);
        }
    }
}
