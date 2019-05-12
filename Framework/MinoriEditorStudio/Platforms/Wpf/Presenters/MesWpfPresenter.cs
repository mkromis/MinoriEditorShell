using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.Platforms.Wpf.Views;
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
        private readonly Stack<FrameworkElement> _navigationStack = new Stack<FrameworkElement>();
        //private HomeView _homeView;

        public MesWpfPresenter(ContentControl mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public override async Task<Boolean> Show(MvxViewModelRequest request)
        {
            try
            {
                IMvxWpfViewLoader loader = Mvx.IoCProvider.Resolve<IMvxWpfViewLoader>();
                FrameworkElement view = loader.CreateView(request);
                return ShowContentView(view);
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

        public override async Task<Boolean> ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxClosePresentationHint)
            {
                //ensure we have at least 2 items on the stack
                //this will be the base view that shows the sliding menu and another view
                //that was selected on the base sliding view
                //ensure we do not pop the base sliding view
                if (_navigationStack.Count >= 2)
                {
                    _navigationStack.Pop();
#warning Fix close hint
                    //if (1 == _navigationStack.Count)
                    //{
                    //    //we have navigated down to the last screen, this is a base
                    //    //view that shows the tab menu, show it                       
                    //    _homeView.ContentGrid.Children.Clear();
                    //    _homeView.ContentGrid.Children.Add(_navigationStack.Peek());
                    //    _mainWindow.Content = _homeView;
                    //}
                }
                return true;
            }
            return await base.ChangePresentation(hint);
        }

        protected Boolean ShowContentView(FrameworkElement frameworkElement)
        {
#warning fix ContentView
            //grab the attribute off of the view
            //RegionType regionName = !(frameworkElement
            //    .GetType()
            //    .GetCustomAttributes(typeof(RegionAttribute), true)
            //    .FirstOrDefault() is RegionAttribute attribute) ? RegionType.Unknown : attribute.Region;

            //based on region decide where we are going to show it
            //switch (regionName)
            //{
            //    case RegionType.BaseTab:
            //        //set the base tab
            //        _mainWindow.Content = frameworkElement;
            //        _homeView = (HomeView)frameworkElement;
            //        break;
            //    case RegionType.Tab:
            //        if (_navigationStack.Any())
            //        {
            //            _navigationStack.Pop();
            //        }

            //        _homeView.ContentGrid.Children.Clear();
            //        _homeView.ContentGrid.Children.Add(frameworkElement);
            //        _navigationStack.Push(frameworkElement);
            //        break;
            //    case RegionType.FullScreen:
            //        //view that requires full screen
            //        //but can navigate backwards
            //        _mainWindow.Content = frameworkElement;
            //        _navigationStack.Push(frameworkElement);
            //        break;
            //}
            return true;
        }
    }
}
