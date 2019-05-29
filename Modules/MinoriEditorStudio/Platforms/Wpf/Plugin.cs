using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.MainMenu.ViewModels;
using MinoriEditorStudio.Modules.Manager.Services;
using MinoriEditorStudio.Modules.Manager.ViewModels;
using MinoriEditorStudio.Modules.Settings;
using MinoriEditorStudio.Modules.Themes.Definitions;
using MinoriEditorStudio.Modules.Themes.Services;
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
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IManager, ManagerViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILayoutItemStatePersister, LayoutItemStatePersister>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeManager, ThemeManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISettingsEditor, MainMenuSettingsViewModel>();

            // Setup manager, is there a better way?
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
