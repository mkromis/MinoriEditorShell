using MinoriEditorStudio.Modules.Themes.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SimpleDemo.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IThemeManager themeManager, IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            themeManager.SetCurrentTheme(themeManager.Themes.First().Name);
        }

        public ICommand TipCalcCommand => new MvxCommand(() => NavigationService.Navigate<TipViewModel>());
    }
}
