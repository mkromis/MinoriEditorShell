using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.IO;

namespace MinoriEditorShell.ViewModels
{
    public class ManagerViewModel : MvxViewModel, IManager
    {
        public event EventHandler ActiveDocumentChanging;
        public event EventHandler ActiveDocumentChanged;

        public IManagerView ManagerView { get; set; }
	    private readonly Boolean _closing;

        private ILayoutItem _activeItem;
	    public ILayoutItem ActiveItem
        {
            get => _activeItem;
            set
            {
                if (SetProperty(ref _activeItem, value))
                {
                    if (value is IDocument document)
                    {
                        SelectedDocument = document;
                    }
                }
            }
        }

        public MvxObservableCollection<ITool> Tools { get; }
        public MvxObservableCollection<IDocument> Documents { get; }

        public Boolean ShowFloatingWindowsInTaskbar
        {
            get => _showFloatingWindowsInTaskbar;
            set
            {
                _showFloatingWindowsInTaskbar = value;
                RaisePropertyChanged(() => ShowFloatingWindowsInTaskbar);
                if (ManagerView != null)
                {
                    ManagerView.UpdateFloatingWindows();
                }
            }
        }

        public virtual String StateFile => @".\ApplicationState.bin";

        public Boolean HasPersistedState => File.Exists(StateFile);

        public IDocument SelectedDocument { get; private set; }

        public ILayoutItemStatePersister LayoutItemStatePersister { get; private set; }

        public ManagerViewModel()
        {
            LayoutItemStatePersister = Mvx.IoCProvider.Resolve<ILayoutItemStatePersister>();

            //((IActivate)this).Activate();

            Tools = new MvxObservableCollection<ITool>();
            Documents = new MvxObservableCollection<IDocument>();
        }

#warning OnViewLoaded(object view)
#if false
        protected override void OnViewLoaded(object view)
	    {
            foreach (var module in _modules)
                foreach (var globalResourceDictionary in module.GlobalResourceDictionaries)
                    Application.Current.Resources.MergedDictionaries.Add(globalResourceDictionary);

	        foreach (var module in _modules)
	            module.PreInitialize();
	        foreach (var module in _modules)
	            module.Initialize();

            // If after initialization no theme was loaded, load the default one
            if (_themeManager.CurrentTheme == null)
            {
                if (!_themeManager.SetCurrentTheme(Properties.Settings.Default.ThemeName))
                {
                    Properties.Settings.Default.ThemeName = (string)Properties.Settings.Default.Properties["ThemeName"].DefaultValue;
                    Properties.Settings.Default.Save();
                    if (!_themeManager.SetCurrentTheme(Properties.Settings.Default.ThemeName))
                    {
                        throw new InvalidOperationException("unable to load application theme");
                    }
                }
            }

            _shellView = (IShellView)view;
            if (!_layoutItemStatePersister.LoadState(this, _shellView, StateFile))
            {
                foreach (var defaultDocument in _modules.SelectMany(x => x.DefaultDocuments))
                    OpenDocument(defaultDocument);
                foreach (var defaultTool in _modules.SelectMany(x => x.DefaultTools))
                    ShowTool((ITool)IoC.GetInstance(defaultTool, null));
            }

            foreach (var module in _modules)
                module.PostInitialize();

            base.OnViewLoaded(view);
        }
#endif

	    public void ShowTool<TTool>()
            where TTool : ITool
	    {
#warning ShowTool<TTool>
            //ShowTool(Mvx.IoCProvider.Resolve<TTool>());
        }

	    public void ShowTool(ITool model)
		{
		    if (Tools.Contains(model))
            {
                model.IsVisible = true;
            }
            else
            {
                Tools.Add(model);
            }

            model.IsSelected = true;
	        ActiveItem = model;
		}

		public void OpenDocument(IDocument model)
		{
#warning OpenDocument(IDocument model)
            //ActivateItem(model);
		}

		public void CloseDocument(IDocument document)
		{
#warning CloseDocument(IDocument document)
            //DeactivateItem(document, true);
        }

        private bool _activateItemGuard = false;
        private bool _showFloatingWindowsInTaskbar;

#warning ActivateItem(IDocument item)
#if false
        public override void ActivateItem(IDocument item)
        {
            if (_closing || _activateItemGuard)
                return;

            _activateItemGuard = true;

            try
            {
                if (ReferenceEquals(item, ActiveItem))
                    return;

                RaiseActiveDocumentChanging();

                var currentActiveItem = ActiveItem;

                base.ActivateItem(item);

                RaiseActiveDocumentChanged();
            }
            finally
            {
                _activateItemGuard = false;
            }
        }
#endif

        private void RaiseActiveDocumentChanging() => ActiveDocumentChanging?.Invoke(this, EventArgs.Empty);

        private void RaiseActiveDocumentChanged() => ActiveDocumentChanged?.Invoke(this, EventArgs.Empty);

#warning OnActivationProcessed(IDocument item, bool success)
#if false
        protected override void OnActivationProcessed(IDocument item, bool success)
        {
            if (!ReferenceEquals(ActiveLayoutItem, item))
                ActiveLayoutItem = item;

            base.OnActivationProcessed(item, success);
        }
#endif

#warning DeactivateItem(IDocument item, bool close)
#if false
        public override void DeactivateItem(IDocument item, bool close)
	    {
	        RaiseActiveDocumentChanging();

	        base.DeactivateItem(item, close);

            RaiseActiveDocumentChanged();
	    }
#endif


#warning OnDeactivate(bool close)
#if false
        protected override void OnDeactivate(bool close)
        {
            // Workaround for a complex bug that occurs when
            // (a) the window has multiple documents open, and
            // (b) the last document is NOT active
            // 
            // The issue manifests itself with a crash in
            // the call to base.ActivateItem(item), above,
            // saying that the collection can't be changed
            // in a CollectionChanged event handler.
            // 
            // The issue occurs because:
            // - Caliburn.Micro sees the window is closing, and calls Items.Clear()
            // - AvalonDock handles the CollectionChanged event, and calls Remove()
            //   on each of the open documents.
            // - If removing a document causes another to become active, then AvalonDock
            //   sets a new ActiveContent.
            // - We have a WPF binding from Caliburn.Micro's ActiveItem to AvalonDock's
            //   ActiveContent property, so ActiveItem gets updated.
            // - The document no longer exists in Items, beacuse that collection was cleared,
            //   but Caliburn.Micro helpfully adds it again - which causes the crash.
            //
            // My workaround is to use the following _closing variable, and ignore activation
            // requests that occur when _closing is true.
            _closing = true;

            _layoutItemStatePersister.SaveState(this, _shellView, StateFile);

            base.OnDeactivate(close);
        }
#endif

        public void Close() => Environment.Exit(0);
    }
}
