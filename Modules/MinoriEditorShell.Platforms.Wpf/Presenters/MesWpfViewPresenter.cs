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
    /// <summary>
    /// Main presenter that allows document and tools to naviage to MesDocumentController
    /// </summary>
    public class MesWpfViewPresenter : MvxWpfViewPresenter
    {
        private IMvxLog _log;

        /// <summary>
        /// Main constructor for the presenter, this gets the main window.
        /// </summary>
        /// <param name="mainWindow"></param>
        public MesWpfViewPresenter(ContentControl mainWindow) : base(mainWindow)
        {
            IMvxLogProvider provider = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            _log = provider.GetLogFor<MesWpfViewPresenter>();
            _log.Trace("Setup: Creating Presenter");

            // Setup main window as singleton
            if (mainWindow is IMesWindow mesWindow)
            {
                _log.Trace("Setting IMesWindow to main window");
                Mvx.IoCProvider.RegisterSingleton<IMesWindow>(mesWindow);
            }
        }

        /// <summary>
        /// This gets called when navigation to view model happens.
        /// Depending on what the type is, will define where the class goes.
        /// Either to MesDocumentManager or main view if not a IMesDocument or IMesTool
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<Boolean> ShowContentView(FrameworkElement element, MvxContentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            try
            {
                // Everything that passes here should be a view
                IMvxView view = element as IMvxView;
                IMesDocumentManager manager = Mvx.IoCProvider.Resolve<IMesDocumentManager>();

                // from which we can now get the view model.
                switch (view.ViewModel)
                {
                    case IMesDocument document:

                        // Try to set view, this is needed for DocumentManager
                        IMesDocument docViewModel = (IMesDocument)view.ViewModel;
                        docViewModel.View = view; // Needed for Binding with AvalonDock

                        // Add to manager model
                        manager.Documents.Add(docViewModel);
                        _log.Trace($"Add {document} to IMesDocumentManager.Documents");
                        return true;

                    case IMesTool tool:
                        // Try to set view, this is needed for DocumentManager
                        IMesTool toolViewModel = (IMesTool)view.ViewModel;
                        toolViewModel.View = view; // Needed for Binding with AvalonDock

                        // Add to manager model
                        manager.Tools.Add(toolViewModel);
                        _log.Trace($"Add {tool} to IDocumentManager.Tools");
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
                throw;
            }
        }
    }
}