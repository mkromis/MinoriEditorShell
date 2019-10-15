using System;
using System.Collections.Generic;
using System.Windows;

namespace MinoriEditorShell.Services
{
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
}
