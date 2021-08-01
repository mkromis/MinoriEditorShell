using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using MinoriEditorShell.Platforms.Avalonia.Presenters.Attributes;
using MinoriEditorShell.Platforms.Avalonia.Views;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MinoriEditorShell.Platforms.Avalonia.Presenters
{
    /// <summary>
    /// Main presenter that allows document and tools to naviage to MesDocumentController
    /// </summary>
    public class MesAvnViewPresenter : MvxAttributeViewPresenter, IMesAvnViewPresenter
    {
        private static readonly ILogger<MesAvnViewPresenter> _log = MvxLogHost.GetLog<MesAvnViewPresenter>();
        private Dictionary<ContentControl, Stack<Control>> _frameworkElementsDictionary;

        private IMesAvnViewLoader _wpfViewLoader;

        /// <summary>
        /// Main constructor for the presenter, this gets the main window.
        /// </summary>
        /// <param name="contentControl"></param>
        public MesAvnViewPresenter(ContentControl contentControl) : base()
        {
            if (contentControl is Window window)
                window.Closed += Window_Closed;

            FrameworkElementsDictionary.Add(contentControl, new Stack<Control>());

            _log.LogTrace("Setup: Creating Presenter");

            // Setup main window as singleton
            if (contentControl is IMesWindow mesWindow)
            {
                _log.LogTrace("Setting IMesWindow to main window");
                Mvx.IoCProvider.RegisterSingleton<IMesWindow>(mesWindow);
            }
        }


        protected MesAvnViewPresenter()
        {
        }

        protected Dictionary<ContentControl, Stack<Control>> FrameworkElementsDictionary
        {
            get
            {
                if (_frameworkElementsDictionary == null)
                    _frameworkElementsDictionary = new Dictionary<ContentControl, Stack<Control>>();
                return _frameworkElementsDictionary;
            }
        }

        protected IMesAvnViewLoader AvnViewLoader
        {
            get
            {
                if (_wpfViewLoader == null)
                    _wpfViewLoader = Mvx.IoCProvider.Resolve<IMesAvnViewLoader>();
                return _wpfViewLoader;
            }
        }

        public override async Task<bool> Close(IMvxViewModel toClose)
        {
            // toClose is window
            if (FrameworkElementsDictionary.Any(i => (i.Key as IMesAvnView)?.ViewModel == toClose) && await CloseWindow(toClose))
                return true;

            // toClose is content
            if (FrameworkElementsDictionary.Any(i => i.Value.Any() && (i.Value.Peek() as IMesAvnView)?.ViewModel == toClose) && await CloseContentView(toClose))
                return true;

            _log.LogWarning($"Could not close ViewModel type {toClose.GetType().Name}");
            return false;
        }

        public override MvxBasePresentationAttribute CreatePresentationAttribute(Type viewModelType, Type viewType)
        {
            if (viewType.IsSubclassOf(typeof(Window)))
            {
                _log.LogTrace($"PresentationAttribute not found for {viewType.Name}. Assuming window presentation");
                return new MesWindowPresentationAttribute();
            }

            _log.LogTrace($"PresentationAttribute not found for {viewType.Name}. Assuming content presentation");
            return new MesContentPresentationAttribute();
        }

        public override MvxBasePresentationAttribute GetOverridePresentationAttribute(MvxViewModelRequest request, Type viewType)
        {
            if (viewType?.GetInterface(nameof(IMvxOverridePresentationAttribute)) != null)
            {
                var viewInstance = AvnViewLoader.CreateView(viewType) as IDisposable;
                using (viewInstance)
                {
                    MvxBasePresentationAttribute presentationAttribute = null;
                    if (viewInstance is IMvxOverridePresentationAttribute overrideInstance)
                        presentationAttribute = overrideInstance.PresentationAttribute(request);

                    if (presentationAttribute == null)
                    {
                        _log.LogWarning("Override PresentationAttribute null. Falling back to existing attribute.");
                    }
                    else
                    {
                        if (presentationAttribute.ViewType == null)
                            presentationAttribute.ViewType = viewType;

                        if (presentationAttribute.ViewModelType == null)
                            presentationAttribute.ViewModelType = request.ViewModelType;

                        return presentationAttribute;
                    }
                }
            }

            return null;
        }

        public override void RegisterAttributeTypes()
        {
            AttributeTypesToActionsDictionary.Register<MesWindowPresentationAttribute>(
                    (viewType, attribute, request) =>
                    {
                        var view = AvnViewLoader.CreateView(request);
                        return ShowWindow(view, (MesWindowPresentationAttribute)attribute, request);
                    },
                    (viewModel, attribute) => CloseWindow(viewModel));

            AttributeTypesToActionsDictionary.Register<MesContentPresentationAttribute>(
                    (viewType, attribute, request) =>
                    {
                        var view = AvnViewLoader.CreateView(request);
                        return ShowContentView(view, (MesContentPresentationAttribute)attribute, request);
                    },
                    (viewModel, attribute) => CloseContentView(viewModel));
        }

        protected virtual Task<bool> CloseContentView(IMvxViewModel toClose)
        {
            var item = FrameworkElementsDictionary.FirstOrDefault(i => i.Value.Any() && (i.Value.Peek() as IMesAvnView)?.ViewModel == toClose);
            var contentControl = item.Key;
            var elements = item.Value;

            if (elements.Any())
                elements.Pop(); // Pop closing view

            if (elements.Any())
            {
                contentControl.Content = elements.Peek();
                return Task.FromResult(true);
            }

            // Close window if no contents
            if (contentControl is Window window)
            {
                FrameworkElementsDictionary.Remove(window);
                window.Close();
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        protected virtual Task<bool> CloseWindow(IMvxViewModel toClose)
        {
            var item = FrameworkElementsDictionary.FirstOrDefault(i => (i.Key as IMesAvnView)?.ViewModel == toClose);
            var contentControl = item.Key;
            if (contentControl is Window window)
            {
                FrameworkElementsDictionary.Remove(window);
                window.Close();
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
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
        protected async Task<Boolean> ShowContentView(
            IMvxView element, MesContentPresentationAttribute attribute, MvxViewModelRequest request)
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
                        _log.LogTrace($"Add {document} to IMesDocumentManager.Documents");
                        return true;

                    case IMesTool tool:
                        // Try to set view, this is needed for DocumentManager
                        IMesTool toolViewModel = (IMesTool)view.ViewModel;
                        toolViewModel.View = view; // Needed for Binding with AvalonDock

                        // Add to manager model
                        manager.Tools.Add(toolViewModel);
                        _log.LogTrace($"Add {tool} to IDocumentManager.Tools");
                        return true;

                    default:
                        _log.LogTrace($"Passing to parent {view.ViewModel}");
                        var contentControl = FrameworkElementsDictionary.Keys.FirstOrDefault(w => (w as MesWindow)?.Identifier == attribute.WindowIdentifier) 
                            ?? FrameworkElementsDictionary.Keys.Last();

                        if (!attribute.StackNavigation && FrameworkElementsDictionary[contentControl].Any())
                            FrameworkElementsDictionary[contentControl].Pop(); // Close previous view

                        FrameworkElementsDictionary[contentControl].Push((Control)element);
                        contentControl.Content = element;
                        return true;
                }
            }
            catch (Exception exception)
            {

                _log.LogError(exception, "Error seen during navigation request to {0} - error {1}",
                    request.ViewModelType.Name, exception.ToLongString());
                throw;
            }
        }

        protected virtual Task<bool> ShowWindow(IMesAvnView element, MesWindowPresentationAttribute attribute, MvxViewModelRequest request)
        {
            Window window;
            if (element is IMesWindow mvxWindow)
            {
                window = (Window)element;
                mvxWindow.Identifier = attribute.Identifier ?? element.GetType().Name;
            }
            else if (element is Window normalWindow)
            {
                // Accept normal Window class
                window = normalWindow;
            }
            else
            {
                // Wrap in window
                window = new MesWindow
                {
                    Identifier = attribute.Identifier ?? element.GetType().Name
                };
            }
            window.Closed += Window_Closed;
            FrameworkElementsDictionary.Add(window, new Stack<Control>());

            if (!(element is Window))
            {
                FrameworkElementsDictionary[window].Push((Control)element);
                window.Content = element;
            }

            if (attribute.Modal)
                throw new NotImplementedException();
                //window.ShowDialog();
            else
                window.Show();
            return Task.FromResult(true);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var window = sender as Window;
            window.Closed -= Window_Closed;

            if (FrameworkElementsDictionary.ContainsKey(window))
                FrameworkElementsDictionary.Remove(window);
        }
    }
}