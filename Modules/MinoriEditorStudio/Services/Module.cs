using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Services
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
