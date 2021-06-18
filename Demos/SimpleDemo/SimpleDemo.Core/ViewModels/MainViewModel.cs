using Microsoft.Extensions.Logging;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Windows.Input;

namespace SimpleDemo.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService) : base(logFactory, navigationService)
        {
            Mvx.IoCProvider.Resolve<IMesWindow>().Title =
                $"Simple Demo v{typeof(App).Assembly.GetName().Version.ToString(3)}";
        }

        public ICommand TipCalcCommand => new MvxCommand(() => NavigationService.Navigate<TipViewModel>());

        public ICommand SettingsCommand => Mvx.IoCProvider.Resolve<IMesSettingsManager>().ShowCommand;
    }
}