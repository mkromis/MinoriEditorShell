using MinoriDemo.Core.Modules.VirtualCanvas.Models;
using MinoriEditorShell.Services;
using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;

namespace MinoriDemo.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        // Handles data context for ribbon.
        private VirtualCanvasViewModel _canvasModel;
        private Color _testcolor = Color.CornflowerBlue;
        private readonly IMesSettingsManager _settingsManager;
        private readonly IMesManager _manager;
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
            //CanvasViewModel.IsClosing += (s, e) => CanvasViewModel = null;
        });

        public Color TestColor
        {
            get => _testcolor;
            set => SetProperty(ref _testcolor, value);
        }

        public ICommand ToolTestCommand => new MvxCommand(() => NavigationService.Navigate<ToolSampleViewModel>());
        //public ICommand TaskRunCommand => new MvxCommand(() => OpenAndFocus<TaskRunTestsViewModel>());

        public ICommand SettingsCommand =>  new MvxCommand(() =>  NavigationService.Navigate(_settingsManager));

        private T OpenAndFocus<T>() where T : MesDocument
        {
            T vm = (T)_manager.Documents.Where(x => x is T).FirstOrDefault();
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
            IMesManager manager, IMesSettingsManager settingsManager,
            IMesStatusBar statusBar)
            : base(logProvider, navigationService)
        {
            _settingsManager = settingsManager;
            _manager = manager;

            _statusBar = statusBar;
            _statusBar.Text = "Ready";

            // Setup settings
            //_settingsManager.Add(new SettingsItem("General", Container.Resolve<GeneralSettings>()));
        }
    }
}
