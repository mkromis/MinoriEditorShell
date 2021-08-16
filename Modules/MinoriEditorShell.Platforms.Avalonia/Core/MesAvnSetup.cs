using Avalonia.Controls;
using MinoriEditorShell.Messages;
using MinoriEditorShell.Modules.Services;
using MinoriEditorShell.Platforms.Avalonia.Presenters;
using MinoriEditorShell.Platforms.Avalonia.Views;
using MinoriEditorShell.Platforms.Avalonia.Services;
using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using MvvmCross.Presenters;
using MinoriEditorShell.Platforms.Wpf.ViewModels;
using Avalonia.Threading;
using System.Reflection;
using System.Linq;
using MvvmCross.Views;
using MvvmCross.Binding;
using MvvmCross.Converters;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Binding.Binders;
using Microsoft.Extensions.Logging;

// Portions of this was barrowed from MvvmCross.
namespace MinoriEditorShell.Platforms.Avalonia
{
    /// <summary>
    /// This is the main initializer for the kit. Call or over-ride this simualr to any other MvxWpfSetup setup
    /// </summary>
    public abstract class MesAvnSetup : MvxSetup, IMvxAvnSetup
    {
        // To handle messages between classes
        private MvvmCross.Plugin.Messenger.IMvxMessenger _messenger;
        private IMesAvnViewPresenter _presenter;
        private ContentControl _root;
        private Dispatcher _uiThreadDispatcher;
        protected IMesAvnViewPresenter Presenter
        {
            get
            {
                _presenter = CreateViewPresenter(_root);
                return _presenter;
            }
        }

        protected virtual IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                List<Assembly> toReturn = new List<Assembly>();
                toReturn.AddRange(GetViewModelAssemblies());
                toReturn.AddRange(GetViewAssemblies());
                return toReturn;
            }
        }

        protected virtual List<Type> ValueConverterHolders => new List<Type>();

        public override IEnumerable<Assembly> GetViewAssemblies()
        {
            return base.GetViewAssemblies().Union(new[] { Assembly.GetEntryAssembly() });
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
        /// Setup class based on presenter
        /// </summary>
        /// <param name="uiThreadDispatcher"></param>
        /// <param name="presenter"></param>
        public void PlatformInitialize(Dispatcher uiThreadDispatcher, IMesAvnViewPresenter presenter)
        {
            _uiThreadDispatcher = uiThreadDispatcher;
            _presenter = presenter;
        }

        /// <summary>
        /// Setup presenter based on content
        /// </summary>
        /// <param name="uiThreadDispatcher"></param>
        /// <param name="root"></param>
        public void PlatformInitialize(Dispatcher uiThreadDispatcher, ContentControl root)
        {
            _uiThreadDispatcher = uiThreadDispatcher;
            _root = root;
        }

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of MvxApplication</returns>
        protected override IMvxApplication CreateApp(IMvxIoCProvider iocProvider) 
        {
            _messenger = iocProvider.Resolve<MvvmCross.Plugin.Messenger.IMvxMessenger>();
            Properties.Settings.Default.PropertyChanged += (s, e) =>
            {
                MesSettingsChangedMessage message = new MesSettingsChangedMessage(
                    s, e.PropertyName,
                    Properties.Settings.Default.PropertyValues[e.PropertyName]);
                _messenger.Publish(message);
            };

            throw new NotImplementedException();
            //return new TApplication();
        }

        protected virtual IMesAvnViewsContainer CreateAvnViewsContainer()
        {
            return new MesAvnViewsContainer();
        }

        protected virtual MvxBindingBuilder CreateBindingBuilder()
        {
            throw new NotImplementedException();
            //return new MvxWindowsBindingBuilder();
        }

        /// <summary>
        /// Used to ensure plugins are loaded.
        /// </summary>
        /// <returns>returns base manager</returns>
        protected override IMvxPluginManager CreatePluginManager(IMvxIoCProvider iocProvider) 
        {
            IMvxPluginManager manager = base.CreatePluginManager(iocProvider);
            manager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            return manager;
        }

        protected override IMvxViewDispatcher CreateViewDispatcher()
        {
            return new MesAvnViewDispatcher(_uiThreadDispatcher, Presenter);
        }

        /// <summary>
        /// Creates the inital view for the setup
        /// </summary>
        /// <param name="root">Control of the main windows for wpf</param>
        /// <returns></returns>
        protected virtual IMesAvnViewPresenter CreateViewPresenter(ContentControl root)
        {
            return new MesAvnViewPresenter(root);
        }

        protected override IMvxViewsContainer CreateViewsContainer(IMvxIoCProvider iocProvider)
        {
            if (iocProvider is null) throw new ArgumentNullException(nameof(iocProvider));

            IMesAvnViewsContainer toReturn = CreateAvnViewsContainer();
            iocProvider.RegisterSingleton<IMesAvnViewLoader>(toReturn);
            return toReturn;
        }
        protected override IMvxNameMapping CreateViewToViewModelNaming()
        {
            return new MvxPostfixAwareViewToViewModelNameMapping("View", "Control");
        }

        protected virtual void FillBindingNames(IMvxBindingNameRegistry registry)
        {
            // this base class does nothing
        }

        protected virtual void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            // this base class does nothing
        }

        protected virtual void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            registry.Fill(ValueConverterAssemblies);
            registry.Fill(ValueConverterHolders);
        }

        protected virtual void InitializeBindingBuilder(IMvxIoCProvider iocProvider)
        {
            RegisterBindingBuilderCallbacks(iocProvider);
            var bindingBuilder = CreateBindingBuilder();
            bindingBuilder.DoRegistration(iocProvider);
        }

        protected void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            RegisterPresenter(iocProvider);
        }

        protected void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            InitializeBindingBuilder(iocProvider);
        }

        /// <summary>
        /// Sets up the dictionary that connects the viewmodel to the view.
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<Type, Type> InitializeLookupDictionary(IMvxIoCProvider iocProvider)
        {
            IDictionary<Type, Type> container = base.InitializeLookupDictionary(iocProvider);
            container.Add(typeof(MesSettingsManagerViewModel), typeof(MesSettingsView));
            container.Add(typeof(MesGeneralSettingsViewModel), typeof(MesGeneralSettingsView));
            return container;
        }
        protected virtual void RegisterBindingBuilderCallbacks(IMvxIoCProvider iocProvider)
        {
            if (iocProvider is null) throw new ArgumentNullException(nameof(iocProvider));

            iocProvider.CallbackWhenRegistered<IMvxValueConverterRegistry>(FillValueConverters);
            iocProvider.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(FillTargetFactories);
            iocProvider.CallbackWhenRegistered<IMvxBindingNameRegistry>(FillBindingNames);
        }

        protected virtual void RegisterPresenter(IMvxIoCProvider iocProvider)
        {
            if (iocProvider is null) throw new ArgumentNullException(nameof(iocProvider));

            var presenter = Presenter;
            iocProvider.RegisterSingleton(presenter);
            iocProvider.RegisterSingleton<IMvxViewPresenter>(presenter);
        }
    }

    /// <summary>
    /// This is the main initializer for the kit. Call or over-ride this simualr to any other MvxWpfSetup setup
    /// </summary>
    /// <typeparam name="TApplication"></typeparam>
    public abstract class MesAvnSetup<TApplication> : MesAvnSetup where TApplication : class, IMvxApplication, new()
    {
        public override IEnumerable<Assembly> GetViewModelAssemblies() => new[] { typeof(TApplication).GetTypeInfo().Assembly };
        protected override IMvxApplication CreateApp(IMvxIoCProvider iocProvider) => iocProvider.IoCConstruct<TApplication>();
        protected override IMvxViewsContainer CreateViewsContainer(IMvxIoCProvider iocProvider) => base.CreateViewsContainer(iocProvider);
    }
}