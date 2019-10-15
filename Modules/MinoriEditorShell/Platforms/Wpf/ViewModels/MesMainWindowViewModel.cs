using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Properties;
using MinoriEditorShell.Services;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
#warning Conductor<IShell>
    [Export(typeof(IMesMainWindow))]
    public class MesMainWindowViewModel : MvxNotifyPropertyChanged, IMesMainWindow, IPartImportsSatisfiedNotification
    {
#pragma warning disable 649
        [Import]
        private readonly IMesManager _shell;

        [Import]
        private readonly IMesResourceManager _resourceManager;

        [Import]
        private readonly IMesCommandKeyGestureService _commandKeyGestureService;
#pragma warning restore 649

        private WindowState _windowState = WindowState.Normal;
        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                _windowState = value;
                RaisePropertyChanged(() => WindowState);
            }
        }

        private Double _width = 1000.0;
        public Double Width
        {
            get => _width;
            set
            {
                _width = value;
                RaisePropertyChanged(() => Width);
            }
        }

        private Double _height = 800.0;
        public Double Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged(() => Height);
            }
        }

        private String _title = Resources.MainWindowDefaultTitle;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private ImageSource _icon;
        public ImageSource Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                RaisePropertyChanged(() => Icon);
            }
        }

        public IMesManager Shell => _shell;

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            if (_icon == null)
                _icon = _resourceManager.GetBitmap("Resources/Icons/MinoriEditorStudio-32.png");
#warning ActivateItem
            //ActivateItem(_shell);
        }

#warning OnViewLoaded
#if false
        protected override void OnViewLoaded(object view)
        {
            _commandKeyGestureService.BindKeyGestures((UIElement) view);
            base.OnViewLoaded(view);
        }
#endif
    }
}
