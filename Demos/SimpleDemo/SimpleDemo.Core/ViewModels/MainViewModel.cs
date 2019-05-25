using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleDemo.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public ICommand TipCalcCommand => new MvxCommand(() => NavigationService.Navigate<TipViewModel>());
    }
}
