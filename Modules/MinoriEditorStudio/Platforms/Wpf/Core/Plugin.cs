using MinoriEditorStudio.Platforms.Wpf.Services;
using MinoriEditorStudio.Platforms.Wpf.Themes;
using MinoriEditorStudio.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;

namespace MinoriEditorStudio.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load() {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<Framework.Services.IManager, Modules.Manager.ViewModels.ManagerViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<Modules.StatusBar.IStatusBar, Modules.StatusBar.ViewModels.StatusBarViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<Modules.Manager.Services.ILayoutItemStatePersister, Modules.Manager.Services.LayoutItemStatePersister>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<Modules.Settings.ISettingsEditor, Modules.MainMenu.ViewModels.MainMenuSettingsViewModel>();

            // Setup manager, is there a better way?
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeManager, ThemeManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeList>(() => new ThemeList
            {
                new BlueTheme(),
                new LightTheme(),
                new DarkTheme(),
            });
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
