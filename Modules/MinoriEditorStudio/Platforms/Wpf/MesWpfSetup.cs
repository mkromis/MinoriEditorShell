using MinoriEditorStudio.Platforms.Wpf.Presenters;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.ViewModels;
using System.Windows.Controls;

namespace MinoriEditorStudio.Platforms.Wpf
{
    public class MesWpfSetup<TApplication> : MvxWpfSetup where TApplication : class, IMvxApplication, new()
    {
        protected override IMvxWpfViewPresenter CreateViewPresenter(ContentControl root) => new MvxWpfViewPresenter(root);

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of MvxApplication</returns>
        protected override IMvxApplication CreateApp() => new TApplication();

        //public class AppBootstrapper : BootstrapperBase
        //{
        //      private List<Assembly> _priorityAssemblies;

        //protected CompositionContainer Container { get; set; }

        //      internal IList<Assembly> PriorityAssemblies
        //      {
        //          get { return _priorityAssemblies; }
        //      }

        //      public AppBootstrapper()
        //      {
        //          this.PreInitialize();
        //          this.Initialize();
        //      }

        //      protected virtual void PreInitialize()
        //      {
        //          var code = Properties.Settings.Default.LanguageCode;

        //          if (!string.IsNullOrWhiteSpace(code))
        //          {
        //              var culture = CultureInfo.GetCultureInfo(code);
        //              // If code == "en", force to use default resource (Resources.resx)
        //              // See PO #243
        //              if (!Translator.Cultures.Contains(culture))
        //                  culture = CultureInfo.InvariantCulture;
        //              Translator.Culture = culture;
        //              Thread.CurrentThread.CurrentUICulture = culture;
        //              Thread.CurrentThread.CurrentCulture = culture;
        //          }
        //      }

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

        //   protected override void OnStartup(object sender, StartupEventArgs e)
        //   {
        //       base.OnStartup(sender, e);
        //          DisplayRootViewFor<IMainWindow>();
        //   }

        //      protected override IEnumerable<Assembly> SelectAssemblies()
        //      {
        //          return new[] { Assembly.GetEntryAssembly() };
        //      }
    }
}
