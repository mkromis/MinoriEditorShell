using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    public class MesGeneralSettingsViewModel : MvxViewModel, IMesSettings
    {
        private readonly IMesThemeManager _themeManager;

        private readonly static List<String> _availableLanguages = new List<String> {
            String.Empty,
            "en",
            "de",
            "ru",
            "zh-Hans",
            "ko",
        };

        private IMesTheme _selectedTheme;
        private String _selectedLanguage;
        private Boolean _autoHideMainMenu;

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

        public String SettingsPageName => Properties.Resources.SettingsPageGeneral;

        public String SettingsPagePath => Properties.Resources.SettingsPathEnvironment;

        public IMvxView View { get; set; }

        public void ApplyChanges()
        {
            Properties.Settings.Default.ThemeName = SelectedTheme.GetType().Name;
            Properties.Settings.Default.LanguageCode = SelectedLanguage;
            Properties.Settings.Default.Save();
        }
    }
}
