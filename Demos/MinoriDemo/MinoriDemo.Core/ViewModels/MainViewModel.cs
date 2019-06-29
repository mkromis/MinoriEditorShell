using MinoriEditorStudio.Services;
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
        private readonly IThemeManager _themeManager;

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IThemeManager themeManager) 
            : base(logProvider, navigationService)
        {
            _themeManager = themeManager;
        }


        public IEnumerable<String> ThemeList => _themeManager.Themes.Select(x => x.Name);

        public String SelectedTheme
        {
            set => _themeManager.SetCurrentTheme(value);
            get => _themeManager.CurrentTheme.Name;
        }
    }
}
