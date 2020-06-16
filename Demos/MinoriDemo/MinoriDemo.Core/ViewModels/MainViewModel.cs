using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace MinoriDemo.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        // Handles data context for ribbon.
        private VirtualCanvasViewModel _canvasModel;
        private Color _testcolor = Color.CornflowerBlue;
        private readonly IMesDocumentManager _manager;
        private readonly IMesStatusBar _statusBar;

        public VirtualCanvasViewModel CanvasModel
        {
            get => _canvasModel;
            set => SetProperty(ref _canvasModel, value);
        }

        public ICommand OpenCanvasCommand => new MvxAsyncCommand(async() =>
        {
            if (CanvasModel == null)
            {
                CanvasModel = new VirtualCanvasViewModel();
                await NavigationService.Navigate(CanvasModel);
            }
            else
            {
                _manager.ActiveItem = CanvasModel;
            }
        });

        public Color TestColor
        {
            get => _testcolor;
            set => SetProperty(ref _testcolor, value);
        }

        public ICommand ToolTestCommand => new MvxCommand(() => NavigationService.Navigate<ToolSampleViewModel>());
        public ICommand ThemeEditorCommand => new MvxCommand(() => NavigationService.Navigate<ThemeEditorViewModel>());

        public ICommand SettingsCommand => Mvx.IoCProvider.Resolve<IMesSettingsManager>().ShowCommand;

        private T OpenAndFocus<T>() where T : MesDocument
        {
            T vm = (T)_manager.Documents.FirstOrDefault(x => x is T);
            if (vm == null)
            {
                vm = Mvx.IoCProvider.Resolve<T>();
                NavigationService.Navigate(vm);
            }

            // if not exist
            _manager.ActiveItem = vm;
            return vm;
        }

        public MainViewModel(
            IMvxLogProvider logProvider, IMvxNavigationService navigationService, 
            IMesDocumentManager manager, IMesStatusBar statusBar)
            : base(logProvider, navigationService)
        {
            _manager = manager;

            _statusBar = statusBar;
            _statusBar.Text = "Ready";

            Mvx.IoCProvider.Resolve<IMesWindow>().DisplayName =
                $"Minori Demo v{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
        }
    }
}
