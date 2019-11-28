using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Platforms.Wpf.Themes;
using MinoriEditorShell.ViewModels;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.Modules.Services;
using MvvmCross.Localization;
using MvvmCross.Plugin.ResxLocalization;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load() {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesManager, MesManagerViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesStatusBar, MesStatusBarViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesLayoutItemStatePersister, MesLayoutItemStatePersister>();
            Mvx.IoCProvider.RegisterType<IMesSettingsManager, MesSettingsViewModel>();

            // Register themes
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesThemeManager, MesThemeManager>();
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
