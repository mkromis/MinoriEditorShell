using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Properties;
using MinoriEditorStudio.Services;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Modules.MainWindow.ViewModels
{
#warning Conductor<IShell>
    [Export(typeof(IMainWindow))]
    public class MainWindowViewModel : MvxNotifyPropertyChanged, IMainWindow, IPartImportsSatisfiedNotification
    {
#pragma warning disable 649
        [Import]
        private IManager _shell;

        [Import]
        private IResourceManager _resourceManager;

        [Import]
        private ICommandKeyGestureService _commandKeyGestureService;
#pragma warning restore 649

        private WindowState _windowState = WindowState.Normal;
        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                RaisePropertyChanged(() => WindowState);
            }
        }

        private double _width = 1000.0;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged(() => Width);
            }
        }

        private double _height = 800.0;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged(() => Height);
            }
        }

        private string _title = Resources.MainWindowDefaultTitle;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private ImageSource _icon;
        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged(() => Icon);
            }
        }

        public IManager Shell
        {
            get { return _shell; }
        }

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
