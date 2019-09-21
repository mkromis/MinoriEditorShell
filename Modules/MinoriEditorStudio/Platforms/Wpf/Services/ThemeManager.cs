using MinoriEditorStudio.Extensions;
using MinoriEditorStudio.Messages;
using MinoriEditorStudio.Platforms.Wpf.Extensions;
using MinoriEditorStudio.Services;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Logging;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    public class ThemeManager : IThemeManager
    {
        // IOC
        private readonly IMvxMessenger _messenger;
        private readonly IMvxLog _log;

        public IEnumerable<ITheme> Themes { get; private set; }

        public ITheme CurrentTheme { get; private set; }

        public ThemeManager(IMvxMessenger messenger, IMvxLogProvider provider)
        {
            Themes = Mvx.IoCProvider.GetAll<ITheme>();

            _messenger = messenger;
            _log = provider.GetLogFor<ThemeManager>();

            String themeName = Properties.Settings.Default.ThemeName;
            if (String.IsNullOrEmpty(themeName)) {
                themeName = GetDefaultApplicationMode();
            }

            SetCurrentTheme(Themes.First().Name, false);

            _messenger.Subscribe<SettingsChangedMessage>((x) =>
            {
                if (x.Name == "ThemeName")
                {
                    SetCurrentTheme(x.Name);
                }
            });
        }

        private String GetDefaultApplicationMode() => null;

        // Needed for Interface
        public Boolean SetCurrentTheme(String name) => SetCurrentTheme(name, true);

        public Boolean SetCurrentTheme(String name, Boolean applySetting)
        {
            try
            {
                // Find theme for name
                ITheme theme = Themes.FirstOrDefault(x => x.Name == name);
                if (theme == null) { return false; }
                CurrentTheme = theme;

                // Setup asnc info
                Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>()
                    .ExecuteOnMainThreadAsync(() =>
                {
                    // Setup app style
                    ResourceDictionary appTheme =
                        Application.Current.Resources.MergedDictionaries.Count > 0
                        ? Application.Current.Resources.MergedDictionaries[0] : null;

                    if (appTheme == null)
                    {
                        appTheme = new ResourceDictionary();
                        Application.Current.Resources.MergedDictionaries.Add(appTheme);
                    }

                    appTheme.BeginInit();

                    appTheme.MergedDictionaries.Clear(); 
                    foreach (Uri uri in theme.ApplicationResources)
                    {
                        ResourceDictionary newDict = new ResourceDictionary { Source = uri };
                        appTheme.MergedDictionaries.Add(newDict);
                    }
                    appTheme.EndInit();
                });

                _log.Info($"Theme set to {name}");

                // publish event
                _messenger.Publish(new ThemeChangeMessage(this, CurrentTheme.Name));

                if (applySetting)
                {
                    Properties.Settings.Default.ThemeName = CurrentTheme.Name;
                }

                return true;

            } catch (Exception e)
            {
                _log.InfoException("Log Theme Setting", e);
                return false;
            }
        }

    }
}
