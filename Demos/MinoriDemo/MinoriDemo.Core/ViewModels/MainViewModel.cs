using Microsoft.Extensions.Logging;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace MinoriDemo.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMesDocumentManager _manager;

        private readonly IMesStatusBar _statusBar;

        // Handles data context for ribbon.
        private VirtualCanvasViewModel _canvasModel;

        private Color _testcolor = Color.CornflowerBlue;

        public MainViewModel(
            IMvxNavigationService navigationService,
            IMesDocumentManager manager, IMesStatusBar statusBar)
        {
            NavigationService = navigationService;
            _manager = manager;

            _statusBar = statusBar;
            _statusBar.Text = "Ready";

            Mvx.IoCProvider.Resolve<IMesWindow>().DisplayName =
                $"Minori Demo v{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
        }

        public VirtualCanvasViewModel CanvasModel
        {
            get => _canvasModel;
            set => SetProperty(ref _canvasModel, value);
        }

        public IMvxNavigationService NavigationService { get; }

        public ICommand OpenCanvasCommand => new MvxAsyncCommand(async () =>
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

        public ICommand SettingsCommand => Mvx.IoCProvider.Resolve<IMesSettingsManager>().ShowCommand;

        public Color TestColor
        {
            get => _testcolor;
            set => SetProperty(ref _testcolor, value);
        }

        public ICommand ThemeEditorCommand => new MvxCommand(() => NavigationService.Navigate<ThemeEditorViewModel>());
        public ICommand ToolTestCommand => new MvxCommand(() => NavigationService.Navigate<ToolSampleViewModel>());
        public Double ZoomValue { get; set; }

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
    }
}