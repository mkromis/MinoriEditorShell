using MinoriEditorStudio.Platforms.Wpf.Services;
using MinoriEditorStudio.Platforms.Wpf.Themes;
using MinoriEditorStudio.ViewModels;
using MinoriEditorStudio.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MinoriEditorStudio.Platforms.Wpf.ViewModels;
using MinoriEditorStudio.Modules.Services;
using MvvmCross.Localization;
using MvvmCross.Plugin.ResxLocalization;

namespace MinoriEditorStudio.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load() {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IManager, ManagerViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IStatusBar, StatusBarViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILayoutItemStatePersister, LayoutItemStatePersister>();
            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISettingsEditor, MainMenuSettingsViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISettingsManager, SettingsViewModel>();

            // I18N
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProvider>(new MvxResxTextProvider(Properties.Resources.ResourceManager));

            // Setup manager, is there a better way?
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeManager, ThemeManager>();
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
