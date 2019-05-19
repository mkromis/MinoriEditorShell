using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.Shell.ViewModels;
using MvvmCross;
using MvvmCross.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorStudio.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxConfigurablePlugin
    {
        public void Configure(IMvxPluginConfiguration configuration) {}

        public void Load() {
            Mvx.IoCProvider.RegisterSingleton<IShell>(() => new ShellViewModel());
        }

//#pragma warning disable 649
//        [Import]
//        private IMainWindow _mainWindow; // <= from presenter

//        [Import]
//        private IShell _shell; // <= also from presenter
//#pragma warning restore 649

//        protected IMainWindow MainWindow
//        {
//            get { return _mainWindow; }
//        }

//        protected IShell Shell
//        {
//            get { return _shell; }
//        }

//        protected IMenu MainMenu
//        {
//            get { return _shell.MainMenu; }
//        }

//        protected IToolBars ToolBars
//        {
//            get { return _shell.ToolBars; }
//        }

//        public virtual IEnumerable<ResourceDictionary> GlobalResourceDictionaries
//        {
//            get { yield break; }
//        }

//        public virtual IEnumerable<IDocument> DefaultDocuments
//        {
//            get { yield break; }
//        }

//        public virtual IEnumerable<Type> DefaultTools
//        {
//            get { yield break; }
//        }
    }
}
