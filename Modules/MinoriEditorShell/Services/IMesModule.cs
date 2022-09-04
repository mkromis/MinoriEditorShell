using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
#warning Not Implemented
#if false
    public interface IMesModule
    {
#warning GlobalResourceDictionaries Disabled

        //IEnumerable<ResourceDictionary> GlobalResourceDictionaries { get; }
        IEnumerable<IMesDocument> DefaultDocuments { get; }

        IEnumerable<Type> DefaultTools { get; }

        void PreInitialize();

        void Initialize();

        void PostInitialize();
    }
#endif
}