using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Reflection;
using System.Windows.Input;

namespace SimpleDemo.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            Mvx.IoCProvider.Resolve<IMesWindow>().Title =
                $"Simple Demo v{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";    
        }

        public ICommand TipCalcCommand => new MvxCommand(() => NavigationService.Navigate<TipViewModel>());

        public ICommand SettingsCommand => new MvxCommand(() =>
        {
            IMesSettingsManager settingsManager = Mvx.IoCProvider.Resolve<IMesSettingsManager>();
            NavigationService.Navigate(settingsManager);
        });
    }
}
