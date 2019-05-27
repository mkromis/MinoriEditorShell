using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using MinoriEditorStudio.Framework;

namespace MinoriEditorStudio.Modules.ToolBars
{
#warning ModuleBase
    [Export(typeof(IModule))]
    public class Module /*: ModuleBase*/
    {
#if false
        public override IEnumerable<ResourceDictionary> GlobalResourceDictionaries
        {
            get
            {
                yield return new ResourceDictionary
                {
                    Source = new Uri("/MinoriEditorStudio;component/Modules/ToolBars/Resources/Styles.xaml", UriKind.Relative)
                };
            }
        }
#endif
    }
}
