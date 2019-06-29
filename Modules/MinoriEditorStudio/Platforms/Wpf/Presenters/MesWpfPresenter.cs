using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Services;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorStudio.Platforms.Wpf.Presenters
{
    public class MesWpfPresenter : MvxWpfViewPresenter
    {
        private IMvxLog _log;
        private readonly ContentControl _mainWindow;
        //private readonly Stack<FrameworkElement> _navigationStack = new Stack<FrameworkElement>();
        //private HomeView _homeView;

        public MesWpfPresenter(ContentControl mainWindow) : base(mainWindow)
        {
            IMvxLogProvider provider = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            _log = provider.GetLogFor<MesWpfPresenter>();
            _log.Trace("ctor");
            _mainWindow = mainWindow;
        }

        protected override async Task<Boolean> ShowContentView(FrameworkElement element, MvxContentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            try
            {
                // Everything that passes here should be a view
                IMvxView view = element as IMvxView;
                IManager manager = Mvx.IoCProvider.Resolve<IManager>();

                // from which we can now get the view model.
                switch(view.ViewModel) {
                    case IDocument document:

                        // Try to set view, this is needed for DocumentManager
                        IDocument docViewModel = (IDocument)view.ViewModel;
                        docViewModel.View = view; // Needed for Binding with AvalonDock
                        docViewModel.ViewAppearing();
                        docViewModel.ViewAppeared();

                        // Add to manager model
                        manager.Documents.Add(docViewModel);
                        _log.Trace($"Add {document.ToString()} to IManager.Documents");
                        return true;

                    case ITool tool:
                        // Try to set view, this is needed for DocumentManager
                        ITool toolViewModel = (ITool)view.ViewModel;
                        toolViewModel.View = view; // Needed for Binding with AvalonDock
                        toolViewModel.ViewAppearing();
                        toolViewModel.ViewAppeared();

                        // Add to manager model
                        manager.Tools.Add(toolViewModel);
                        _log.Trace($"Add {tool.ToString()} to IManager.Tools");
                        return true;

                    default:
                        return await base.ShowContentView(element, attribute, request);
                }
            }
            catch (Exception exception)
            {
                if (_log == null)
                {
                    _log = Mvx.IoCProvider.Resolve<IMvxLog>();
                }
                _log.ErrorException("Error seen during navigation request to {0} - error {1}",
                    exception, request.ViewModelType.Name, exception.ToLongString());
                return false;
            }
        }
    }
}
