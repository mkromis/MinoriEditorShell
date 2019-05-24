using MinoriEditorStudio.Framework.Attributes;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _log.Info("ctor");
            _mainWindow = mainWindow;
        }

        public override async Task<Boolean> Show(MvxViewModelRequest request)
        {
            try
            {
                IMvxWpfViewLoader loader = Mvx.IoCProvider.Resolve<IMvxWpfViewLoader>();
                FrameworkElement view = loader.CreateView(request);
                //ShowContentView(view);
                return await base.Show(request);
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

        protected override MvxPresentationAttributeAction GetPresentationAttributeAction(MvxViewModelRequest request, out MvxBasePresentationAttribute attribute) => 
            base.GetPresentationAttributeAction(request, out attribute);

        public override MvxBasePresentationAttribute GetOverridePresentationAttribute(MvxViewModelRequest request, Type viewType) => 
            base.GetOverridePresentationAttribute(request, viewType);

        public override void RegisterAttributeTypes() => 
            base.RegisterAttributeTypes();

        public override MvxBasePresentationAttribute GetPresentationAttribute(MvxViewModelRequest request) => 
            base.GetPresentationAttribute(request);

        public override MvxBasePresentationAttribute CreatePresentationAttribute(Type viewModelType, Type viewType) => 
            base.CreatePresentationAttribute(viewModelType, viewType);

        protected override Task<Boolean> CloseContentView(IMvxViewModel toClose) => 
            base.CloseContentView(toClose);

        public override Task<Boolean> Close(IMvxViewModel toClose) => 
            base.Close(toClose);

        protected override Task<Boolean> CloseWindow(IMvxViewModel toClose) => 
            base.CloseWindow(toClose);

        protected override Task<Boolean> ShowWindow(FrameworkElement element, MvxWindowPresentationAttribute attribute, MvxViewModelRequest request) => 
            base.ShowWindow(element, attribute, request);

#warning Fix close hint
        public override async Task<Boolean> ChangePresentation(MvxPresentationHint hint)
        {
            //if (hint is MvxClosePresentationHint)
            //{
            //    //ensure we have at least 2 items on the stack
            //    //this will be the base view that shows the sliding menu and another view
            //    //that was selected on the base sliding view
            //    //ensure we do not pop the base sliding view
            //    if (_navigationStack.Count >= 2)
            //    {
            //        _navigationStack.Pop();
            //        //if (1 == _navigationStack.Count)
            //        //{
            //        //    //we have navigated down to the last screen, this is a base
            //        //    //view that shows the tab menu, show it                       
            //        //    _homeView.ContentGrid.Children.Clear();
            //        //    _homeView.ContentGrid.Children.Add(_navigationStack.Peek());
            //        //    _mainWindow.Content = _homeView;
            //        //}
            //    }
            //    return true;
            //}
            return await base.ChangePresentation(hint);
        }

#warning fix ContentView
        protected override async Task<Boolean> ShowContentView(FrameworkElement element, MvxContentPresentationAttribute attribute, MvxViewModelRequest request)
        //protected void ShowContentView(FrameworkElement frameworkElement)
        {
            // grab the attribute off of the view
            //RegionType regionName = !(frameworkElement
            //    .GetType()
            //    .GetCustomAttributes(typeof(RegionAttribute), true)
            //    .FirstOrDefault() is RegionAttribute attribute) ? RegionType.Unknown : attribute.Region;

            //var result = element.GetType().GetCustomAttributes(typeof(ManagerAttribute), true);
            //var result2 = element.FindName("Manager");

            //based on region decide where we are going to show it
            //switch (regionName)
            //{
            //    //    case RegionType.BaseTab:
            //    //        //set the base tab
            //    //        _mainWindow.Content = frameworkElement;
            //    //        _homeView = (HomeView)frameworkElement;
            //    //        break;
            //    //    case RegionType.Tab:
            //    //        if (_navigationStack.Any())
            //    //        {
            //    //            _navigationStack.Pop();
            //    //        }

            //    //        _homeView.ContentGrid.Children.Clear();
            //    //        _homeView.ContentGrid.Children.Add(frameworkElement);
            //    //        _navigationStack.Push(frameworkElement);
            //    //        break;
            //    //    case RegionType.FullScreen:
            //    //        //view that requires full screen
            //    //        //but can navigate backwards
            //    //        _mainWindow.Content = frameworkElement;
            //    //        _navigationStack.Push(frameworkElement);
            //    //        break;
            //}
            return await base.ShowContentView(element, attribute, request);
        }
    }
}
