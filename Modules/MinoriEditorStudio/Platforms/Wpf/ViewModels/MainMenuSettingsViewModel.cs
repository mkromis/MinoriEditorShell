using MinoriEditorStudio.Modules.Settings;
using MinoriEditorStudio.Services;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainMenuSettingsViewModel : MvxNotifyPropertyChanged, ISettingsEditor
    {
        private readonly IThemeManager _themeManager;

        private readonly static List<String> _availableLanguages = new List<String> {
            String.Empty,
            "en",
            "de",
            "ru",
            "zh-Hans",
            "ko",
        };

        private ITheme _selectedTheme;
        private String _selectedLanguage;
        private Boolean _autoHideMainMenu;

        public MainMenuSettingsViewModel()
        {
            _themeManager = Mvx.IoCProvider.Resolve<IThemeManager>();
            SelectedTheme = _themeManager.CurrentTheme;
            AutoHideMainMenu = Properties.Settings.Default.AutoHideMainMenu;
            SelectedLanguage = Properties.Settings.Default.LanguageCode;
        }

        public IEnumerable<ITheme> Themes => _themeManager.Themes;

        public ITheme SelectedTheme
        {
            get => _selectedTheme;
            set => SetProperty(ref _selectedTheme, value);
        }

        public IEnumerable<String> Languages => _availableLanguages;

        public String SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }

        public Boolean AutoHideMainMenu
        {
            get => _autoHideMainMenu;
            set
            {
                if (value.Equals(_autoHideMainMenu)) { return; }
                SetProperty(ref _autoHideMainMenu, value);
            }
        }

        public String SettingsPageName => Properties.Resources.SettingsPageGeneral;

        public String SettingsPagePath => Properties.Resources.SettingsPathEnvironment;

        public void ApplyChanges()
        {
            Properties.Settings.Default.ThemeName = SelectedTheme.GetType().Name;
            Properties.Settings.Default.AutoHideMainMenu = AutoHideMainMenu;
            Properties.Settings.Default.LanguageCode = SelectedLanguage;
            Properties.Settings.Default.Save();
        }
    }
}
