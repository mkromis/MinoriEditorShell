using MinoriEditorShell.Messages;
using MinoriEditorShell.Modules.Services;
using MinoriEditorShell.Platforms.Wpf.Presenters;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.Platforms.Wpf.Views;
using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf
{
    /// <summary>
    /// This is the main initializer for the kit. Call or over-ride this simualr to any other MvxWpfSetup setup
    /// </summary>
    /// <typeparam name="TApplication"></typeparam>
    public class MesWpfSetup<TApplication> : MvxWpfSetup where TApplication : class, IMvxApplication, new()
    {
        // To handle messages between classes
        private MvvmCross.Plugin.Messenger.IMvxMessenger _messenger;

        /// <summary>
        /// Creates the inital view for the setup
        /// </summary>
        /// <param name="root">Control of the main windows for wpf</param>
        /// <returns></returns>
        protected override IMvxWpfViewPresenter CreateViewPresenter(ContentControl root)
        {
            // This handles main window.
            return new MesWpfViewPresenter(root);
        }

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of MvxApplication</returns>
        protected override IMvxApplication CreateApp()
        {
            _messenger = Mvx.IoCProvider.Resolve<MvvmCross.Plugin.Messenger.IMvxMessenger>();
            Properties.Settings.Default.PropertyChanged += (s, e) =>
            {
                MesSettingsChangedMessage message = new MesSettingsChangedMessage(
                    s, e.PropertyName,
                    Properties.Settings.Default.PropertyValues[e.PropertyName]);
                _messenger.Publish(message);
            };

            return new TApplication();
        }

        /// <summary>
        /// Load any additional plugins, calling parent
        /// </summary>
        /// <param name="pluginManager"></param>
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
        }

        /// <summary>
        /// Sets up the dictionary that connects the viewmodel to the view.
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<Type, Type> InitializeLookupDictionary()
        {
            IDictionary<Type, Type> container = base.InitializeLookupDictionary();
            container.Add(typeof(MesSettingsManagerViewModel), typeof(MesSettingsView));
            container.Add(typeof(MesGeneralSettingsViewModel), typeof(MesGeneralSettingsView));
            return container;
        }

        /// <summary>
        /// Used to ensure plugins are loaded.
        /// </summary>
        /// <returns>returns base manager</returns>
        protected override IMvxPluginManager CreatePluginManager()
        {
            IMvxPluginManager manager = base.CreatePluginManager();
            manager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            return manager;
        }

        /// <summary>
        /// Sets up initial connected types and setup
        /// </summary>
        public override void InitializePrimary()
        {
            base.InitializePrimary();

            // register necessary interfaces
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesDocumentManager, MesDocumentManagerViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesStatusBar, MesStatusBarViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesLayoutItemStatePersister, MesLayoutItemStatePersister>();
            Mvx.IoCProvider.RegisterType<IMesSettingsManager, MesSettingsManagerViewModel>();

            // Register themes
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMesThemeManager, MesThemeManager>();

            // try to setup culture info
            String code = Properties.Settings.Default.LanguageCode;

            if (!String.IsNullOrWhiteSpace(code))
            {
                CultureInfo culture = CultureInfo.GetCultureInfo(code);
                // If code == "en", force to use default resource (Resources.resx)
                // See PO #243
#warning fix translator depends
                //if (!Translator.Cultures.Contains(culture))
                //    culture = CultureInfo.InvariantCulture;
                //Translator.Culture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }
    }
}