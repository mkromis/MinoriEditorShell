using System;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Used for interop with code that requires an IServiceProvider. This class
    /// defers to the MEF container to resolve services.
    /// </summary>
    [Export(typeof(IServiceProvider))]
    public class MesServiceProvider : IServiceProvider
    {
        /// <summary>
        /// Looks up the specified service.
        /// </summary>
        public object GetService(Type serviceType)
        {
#warning GetService
            throw new NotImplementedException();
            //return IoC.GetInstance(serviceType, null);
        }
    }
}
