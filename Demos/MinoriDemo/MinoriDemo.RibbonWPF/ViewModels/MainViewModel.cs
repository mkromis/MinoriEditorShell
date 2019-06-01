using MinoriDemo.RibbonWPF.Modules.VirtualCanvas.ViewModels;
using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.Themes.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MinoriDemo.RibbonWPF.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        // Handles data context for ribbon.
        private VirtualCanvasViewModel _canvasViewModel;
        //private readonly ISettingsManager _settingsManager;
        //private readonly IThemeSettings _themeSettings;
        private readonly IThemeManager _themeManager;
        private readonly IManager _manager;

        public VirtualCanvasViewModel CanvasViewModel
        {
            get => _canvasViewModel;
            set => SetProperty(ref _canvasViewModel, value);
        }

        public ICommand OpenCanvasCommand => new MvxCommand(() =>
        {
            CanvasViewModel = OpenAndFocus<VirtualCanvasViewModel>();
            CanvasViewModel.IsClosing += (s, e) => CanvasViewModel = null;
        });

        //public ICommand TaskRunCommand => new DelegateCommand(() => OpenAndFocus<TaskRunTestsViewModel>());

        //public ICommand SettingsCommand => _settingsManager.SettingsCommand;

        private T OpenAndFocus<T>() where T : Document
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

        public IEnumerable<String> ThemeList => _themeManager.Themes.Select(x => x.Name);

        public String SelectedTheme
        {
            set => _themeManager.SetCurrentTheme(value);
            get => _themeManager.CurrentTheme.Name;
        }

        public MainViewModel(
            IMvxLogProvider logProvider, IMvxNavigationService navigationService, 
            IManager manager, IThemeManager themeManager)
            : base(logProvider, navigationService)
        {
            _themeManager = themeManager;
            _themeManager.SetCurrentTheme("Blue");
            _manager = manager;

            //_settingsManager = Container.Resolve<ISettingsManager>();
            //_themeSettings = Container.Resolve<IThemeSettings>();
            //_themeManager = Container.Resolve<IThemeManager>();

            //// Setup theme
            //String themeName = _themeSettings.SelectedTheme;
            //if (themeName == "Default")
            //{
            //    themeName = _themeSettings.GetSystemTheme();
            //}
            //_themeManager.SetCurrent(themeName);

            //// Setup settings
            //_settingsManager.Add(new SettingsItem("General", Container.Resolve<GeneralSettings>()));
        }
    }
}
