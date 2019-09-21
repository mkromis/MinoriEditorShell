using MinoriEditorStudio.Services;
using MvvmCross;
using MvvmCross.Localization;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainMenuSettingsViewModel : MvxViewModel, ISettingsEditor
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
            set {
                if(SetProperty(ref _selectedTheme, value))
                {
                    _themeManager.SetCurrentTheme(_selectedTheme.Name);
                }
            }
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

        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("", GetType().Name);

        public IMvxView View { get; set; }

        public void ApplyChanges()
        {
            Properties.Settings.Default.ThemeName = SelectedTheme.GetType().Name;
            Properties.Settings.Default.AutoHideMainMenu = AutoHideMainMenu;
            Properties.Settings.Default.LanguageCode = SelectedLanguage;
            Properties.Settings.Default.Save();
        }
    }
}
