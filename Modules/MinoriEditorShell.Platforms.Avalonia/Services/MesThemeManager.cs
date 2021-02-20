using MinoriEditorShell.Extensions;
using MinoriEditorShell.Messages;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Logging;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MinoriEditorShell.Platforms.Avalonia.Services
{
    public class MesThemeManager : IMesThemeManager
    {
        // IOC
        private readonly IMvxMessenger _messenger;

        private readonly IMvxLog _log;

        public IEnumerable<IMesTheme> Themes { get; private set; }

        public IMesTheme CurrentTheme { get; private set; }

        public MesThemeManager(IMvxMessenger messenger, IMvxLogProvider provider)
        {
            _log = provider.GetLogFor<MesThemeManager>();
            _messenger = messenger;

            Themes = Mvx.IoCProvider.GetAll<IMesTheme>();

            _messenger.Subscribe<MesSettingsChangedMessage>((x) =>
            {
                if (x.Name == "ThemeName")
                {
                    SetCurrentTheme(x.Name);
                }
            });
        }

        // Needed for Interface
        public Boolean SetCurrentTheme(String name) => SetCurrentTheme(name, true);

        public Boolean SetCurrentTheme(String name, Boolean applySetting)
        {
            try
            {
                // Find theme for name
                IMesTheme theme = Themes.FirstOrDefault(x => x.Name == name);
                if (theme == null) { return false; }
                CurrentTheme = theme;

                // Setup asnc info
                Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>()
                    .ExecuteOnMainThreadAsync(() =>
                {
                    // // Setup app style
                    // ResourceDictionary appTheme =
                    //     Application.Current.Resources.MergedDictionaries.Count > 0
                    //     ? Application.Current.Resources.MergedDictionaries[0] : null;

                    // if (appTheme == null)
                    // {
                    //     appTheme = new ResourceDictionary();
                    //     Application.Current.Resources.MergedDictionaries.Add(appTheme);
                    // }

                    // appTheme.BeginInit();

                    // appTheme.MergedDictionaries.Clear();
                    // foreach (Uri uri in theme.ApplicationResources)
                    // {
                    //     ResourceDictionary newDict = new ResourceDictionary { Source = uri };
                    //     appTheme.MergedDictionaries.Add(newDict);
                    // }
                    // appTheme.EndInit();
                });

                _log.Info($"Theme set to {name}");

                // publish event
                _messenger.Publish(new MesThemeChangeMessage(this, CurrentTheme.Name));

                if (applySetting)
                {
                    Properties.Settings.Default.ThemeName = CurrentTheme.GetType().Name;
                    Properties.Settings.Default.Save();
                }

                return true;
            }
            catch (Exception e)
            {
                _log.InfoException("Log Theme Setting", e);
                return false;
            }
        }
    }
}