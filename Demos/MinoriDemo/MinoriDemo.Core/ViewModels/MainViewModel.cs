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

namespace MinoriDemo.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IThemeManager themeManager) 
            : base(logProvider, navigationService)
        {
            themeManager.SetCurrentTheme("Blue");
        }
    }
}
