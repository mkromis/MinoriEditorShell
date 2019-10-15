using MinoriEditorShell.Services;
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

namespace MinoriEditorShell.Platforms.Wpf.Presenters
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
            _log.Trace("Setup: Creating Presenter");
            _mainWindow = mainWindow;
            
        }

        protected override async Task<Boolean> ShowContentView(FrameworkElement element, MvxContentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            try
            {
                // Everything that passes here should be a view
                IMvxView view = element as IMvxView;
                IMesManager manager = Mvx.IoCProvider.Resolve<IMesManager>();

                // from which we can now get the view model.
                switch(view.ViewModel) {
                    case IMesDocument document:

                        // Try to set view, this is needed for DocumentManager
                        IMesDocument docViewModel = (IMesDocument)view.ViewModel;
                        docViewModel.View = view; // Needed for Binding with AvalonDock
                        docViewModel.ViewAppearing();
                        docViewModel.ViewAppeared();

                        // Add to manager model
                        manager.Documents.Add(docViewModel);
                        _log.Trace($"Add {document.ToString()} to IManager.Documents");
                        return true;

                    case IMesTool tool:
                        // Try to set view, this is needed for DocumentManager
                        IMesTool toolViewModel = (IMesTool)view.ViewModel;
                        toolViewModel.View = view; // Needed for Binding with AvalonDock
                        toolViewModel.ViewAppearing();
                        toolViewModel.ViewAppeared();

                        // Add to manager model
                        manager.Tools.Add(toolViewModel);
                        _log.Trace($"Add {tool.ToString()} to IManager.Tools");
                        return true;

                    default:
                        _log.Trace($"Passing to parent {view.ViewModel.ToString()}");
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
