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
using System.Reflection;
using System.Threading;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf
{
    /// <summary>
    /// This is the main initializer for the kit. Call or over-ride this simualr to any other MvxWpfSetup setup
    /// </summary>
    /// <typeparam name="TApplication"></typeparam>
    public abstract class MesWpfSetup : MvxWpfSetup
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
            return new MesWpfPresenter(root);
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
        protected override IDictionary<Type, Type> InitializeLookupDictionary(IMvxIoCProvider iocProvider)
        {
            IDictionary<Type, Type> container = base.InitializeLookupDictionary(iocProvider);
            container.Add(typeof(MesSettingsManagerViewModel), typeof(MesSettingsView));
            container.Add(typeof(MesGeneralSettingsViewModel), typeof(MesGeneralSettingsView));
            return container;
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

        /// <summary>
        /// Sets up initial connected types and setup
        /// </summary>
        public override void InitializePrimary() {
            base.InitializePrimary();

            // register necessary interfaces
            IMvxIoCProvider iocProvider = Mvx.IoCProvider;
            iocProvider.LazyConstructAndRegisterSingleton<IMesDocumentManager, MesDocumentManagerViewModel>();
            iocProvider.LazyConstructAndRegisterSingleton<IMesStatusBar, MesStatusBarViewModel>();
            iocProvider.LazyConstructAndRegisterSingleton<IMesLayoutItemStatePersister, MesLayoutItemStatePersister>();
            iocProvider.RegisterType<IMesSettingsManager, MesSettingsManagerViewModel>();

            // Register themes
            iocProvider.LazyConstructAndRegisterSingleton<IMesThemeManager, MesThemeManager>();

            // try to setup culture info
            string code = Properties.Settings.Default.LanguageCode;

            if (!string.IsNullOrWhiteSpace(code))
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
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of MvxApplication</returns>
        public override void InitializeSecondary() {
            base.InitializeSecondary();

            _messenger = Mvx.IoCProvider.Resolve<MvvmCross.Plugin.Messenger.IMvxMessenger>();
            Properties.Settings.Default.PropertyChanged += (s, e) =>
            {
                MesSettingsChangedMessage message = new MesSettingsChangedMessage(
                    s, e.PropertyName,
                    Properties.Settings.Default.PropertyValues[e.PropertyName]);
                _messenger.Publish(message);
            };
        }
    }
    public abstract class MesWpfSetup<TApplication> : MesWpfSetup where TApplication : class, IMvxApplication, new()
    {
        protected override IMvxApplication CreateApp(IMvxIoCProvider iocProvider) => iocProvider.IoCConstruct<TApplication>();
        public override IEnumerable<Assembly> GetViewModelAssemblies() => new[] { typeof(TApplication).GetTypeInfo().Assembly };
    }

    ///// <summary>
    ///// By default, we are configured to use MEF
    ///// </summary>
    //protected override void Configure()
    //{
    //          // Add all assemblies to AssemblySource (using a temporary DirectoryCatalog).
    //          var directoryCatalog = new DirectoryCatalog(@"./");
    //          AssemblySource.Instance.AddRange(
    //              directoryCatalog.Parts
    //                  .Select(part => ReflectionModelServices.GetPartType(part).Value.Assembly)
    //                  .Where(assembly => !AssemblySource.Instance.Contains(assembly)));

    //          // Prioritise the executable assembly. This allows the client project to override exports, including IShell.
    //          // The client project can override SelectAssemblies to choose which assemblies are prioritised.
    //          _priorityAssemblies = SelectAssemblies().ToList();
    //    var priorityCatalog = new AggregateCatalog(_priorityAssemblies.Select(x => new AssemblyCatalog(x)));
    //    var priorityProvider = new CatalogExportProvider(priorityCatalog);

    //          // Now get all other assemblies (excluding the priority assemblies).
    //	var mainCatalog = new AggregateCatalog(
    //              AssemblySource.Instance
    //                  .Where(assembly => !_priorityAssemblies.Contains(assembly))
    //                  .Select(x => new AssemblyCatalog(x)));
    //    var mainProvider = new CatalogExportProvider(mainCatalog);

    //	Container = new CompositionContainer(priorityProvider, mainProvider);
    //    priorityProvider.SourceProvider = Container;
    //    mainProvider.SourceProvider = Container;

    //	var batch = new CompositionBatch();

    //    BindServices(batch);
    //          batch.AddExportedValue(mainCatalog);

    //	Container.Compose(batch);
    //}

    //   protected virtual void BindServices(CompositionBatch batch)
    //      {
    //          batch.AddExportedValue<IWindowManager>(new WindowManager());
    //          batch.AddExportedValue<IEventAggregator>(new EventAggregator());
    //          batch.AddExportedValue(Container);
    //       batch.AddExportedValue(this);
    //      }

    //protected override object GetInstance(Type serviceType, string key)
    //{
    //	string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
    //	var exports = Container.GetExports<object>(contract);

    //	if (exports.Any())
    //		return exports.First().Value;

    //	throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
    //}

    //protected override IEnumerable<object> GetAllInstances(Type serviceType)
    //{
    //	return Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
    //}

    //protected override void BuildUp(object instance)
    //{
    //	Container.SatisfyImportsOnce(instance);
    //}

    //      protected override IEnumerable<Assembly> SelectAssemblies()
    //      {
    //          return new[] { Assembly.GetEntryAssembly() };
    //      }
}