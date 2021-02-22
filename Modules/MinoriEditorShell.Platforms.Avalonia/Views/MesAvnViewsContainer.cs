using Avalonia;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public class MesAvnViewsContainer
        : MvxViewsContainer
        , IMesAvnViewsContainer
    {
        public virtual StyledElement CreateView(MvxViewModelRequest request)
        {
            var viewType = GetViewType(request.ViewModelType);
            if (viewType == null)
                throw new MvxException("View Type not found for " + request.ViewModelType);

            var wpfView = CreateView(viewType) as IMesAvnView;

            if (request is MvxViewModelInstanceRequest instanceRequest)
            {
                wpfView.ViewModel = instanceRequest.ViewModelInstance;
            }
            else
            {
                var viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                wpfView.ViewModel = viewModelLoader.LoadViewModel(request, null);
            }

            return wpfView as StyledElement;
        }

        public StyledElement CreateView(Type viewType)
        {
            var viewObject = Activator.CreateInstance(viewType);
            if (viewObject == null)
                throw new MvxException("View not loaded for " + viewType);

            var wpfView = viewObject as IMesAvnView;
            if (wpfView == null)
                throw new MvxException("Loaded View does not have IMvxWpfView interface " + viewType);

            var viewControl = viewObject as StyledElement;
            if (viewControl == null)
                throw new MvxException("Loaded View is not a FrameworkElement " + viewType);

            return viewControl;
        }
    }
}

