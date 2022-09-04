using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.ViewModels
{
    public class MesGeneralSettingsViewModel : MvxViewModel, IMesSettings
    {
        private readonly IMesThemeManager _themeManager;

        private readonly static List<string> _availableLanguages = new List<string> {
            string.Empty,
            "en",
            "de",
            "ru",
            "zh-Hans",
            "ko",
        };

        private IMesTheme _selectedTheme;
        private string _selectedLanguage;

        public MesGeneralSettingsViewModel()
        {
            _themeManager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            SelectedTheme = _themeManager.CurrentTheme;
            SelectedLanguage = Properties.Settings.Default.LanguageCode;
        }

        public IEnumerable<IMesTheme> Themes => _themeManager.Themes;

        public IMesTheme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    _themeManager.SetCurrentTheme(_selectedTheme.Name);
                }
            }
        }

        public IEnumerable<string> Languages => _availableLanguages;

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }

        public string SettingsPageName => Properties.Resources.SettingsPageGeneral;

        public string SettingsPagePath => Properties.Resources.SettingsPathEnvironment;

        public IMvxView View { get; set; }

        public void ApplyChanges()
        {
            Properties.Settings.Default.ThemeName = SelectedTheme.GetType().Name;
            Properties.Settings.Default.LanguageCode = SelectedLanguage;
            Properties.Settings.Default.Save();
        }
    }
}