using System.Collections.Generic;
using System.ComponentModel.Composition;
using MinoriEditorStudio.Framework.Themes;
using MinoriEditorStudio.Modules.Settings;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Modules.MainMenu.ViewModels
{
    [Export(typeof (ISettingsEditor))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainMenuSettingsViewModel : MvxNotifyPropertyChanged, ISettingsEditor
    {
        private readonly IThemeManager _themeManager;

        private readonly static List<string> _availableLanguages = new List<string> {
            string.Empty,
            "en",
            "de",
            "ru",
            "zh-Hans",
            "ko",
        };

        private ITheme _selectedTheme;
        private string _selectedLanguage;
        private bool _autoHideMainMenu;

        [ImportingConstructor]
        public MainMenuSettingsViewModel(IThemeManager themeManager)
        {
            _themeManager = themeManager;
            SelectedTheme = themeManager.CurrentTheme;
            AutoHideMainMenu = Properties.Settings.Default.AutoHideMainMenu;
            SelectedLanguage = Properties.Settings.Default.LanguageCode;
        }

        public IEnumerable<ITheme> Themes => _themeManager.Themes;

        public ITheme SelectedTheme
        {
            get => _selectedTheme;
            set => SetProperty(ref _selectedTheme, value);
        }

        public IEnumerable<string> Languages => _availableLanguages;

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }

        public bool AutoHideMainMenu
        {
            get { return _autoHideMainMenu; }
            set
            {
                if (value.Equals(_autoHideMainMenu)) return;
                _autoHideMainMenu = value;
                RaisePropertyChanged(() => AutoHideMainMenu);
            }
        }

        public string SettingsPageName
        {
            get { return Properties.Resources.SettingsPageGeneral; }
        }

        public string SettingsPagePath
        {
            get { return Properties.Resources.SettingsPathEnvironment; }
        }

        public void ApplyChanges()
        {
            Properties.Settings.Default.ThemeName = SelectedTheme.GetType().Name;
            Properties.Settings.Default.AutoHideMainMenu = AutoHideMainMenu;
            Properties.Settings.Default.LanguageCode = SelectedLanguage;
            Properties.Settings.Default.Save();
        }
    }
}
