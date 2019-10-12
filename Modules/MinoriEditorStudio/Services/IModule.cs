using System;
using System.Collections.Generic;
using System.Windows;

namespace MinoriEditorShell.Services
{
	public interface IModule
	{
#warning GlobalResourceDictionaries Disabled
        //IEnumerable<ResourceDictionary> GlobalResourceDictionaries { get; }
        IEnumerable<IDocument> DefaultDocuments { get; }
        IEnumerable<Type> DefaultTools { get; }

        void PreInitialize();
		void Initialize();
        void PostInitialize();
	}
}
