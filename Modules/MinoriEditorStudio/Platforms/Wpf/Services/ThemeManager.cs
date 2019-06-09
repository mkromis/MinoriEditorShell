/*
 * Original source code from the Wide framework:
 * https://github.com/chandramouleswaran/Wide
 * 
 * Used in MinoriEditorStudio with kind permission of the author.
 *
 * Original licence follows:
 *
 * Copyright (c) 2013 Chandramouleswaran Ravichandran
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Messages;
using MinoriEditorStudio.Modules.Themes.Definitions;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MinoriEditorStudio.Modules.Themes.Services
{
    public class ThemeManager : IThemeManager
    {
        // IOC
        private readonly IMvxMessenger _messenger;

        public IThemeList Themes { get; private set; }

        public ITheme CurrentTheme { get; private set; }

        public ThemeManager(IMvxMessenger messenger, IThemeList themeList)
        {
            Themes = themeList;
            _messenger = messenger;

            String themeName = Properties.Settings.Default.ThemeName;
            if (String.IsNullOrEmpty(themeName)) {
                themeName = GetDefaultApplicationMode();
            }

            SetCurrentTheme(Properties.Resources.ThemeBlueName, false);

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
            // Find theme for name
            ITheme theme = Themes.FirstOrDefault(x => x.Name == name);
            if (theme == null) { return false; }
            CurrentTheme = theme;

            Application.Current.Dispatcher.InvokeAsync(() =>
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

                appTheme.MergedDictionaries.Clear();
                appTheme.BeginInit();

                foreach (Uri uri in theme.ApplicationResources)
                {
                    ResourceDictionary newDict = new ResourceDictionary { Source = uri };
                    appTheme.MergedDictionaries.Add(newDict);
                }
                appTheme.EndInit();

                //_logger.Log($"Theme set to {name}", Category.Info, Priority.None);
                //_eventAggregator.GetEvent<ThemeChangeEvent>().Publish(newTheme);
            });

            //private ResourceDictionary applicationResourceDictionary;
            //if (_applicationResourceDictionary == null)
            //{
            //    _applicationResourceDictionary = new ResourceDictionary();
            //    Application.Current.Resources.MergedDictionaries.Add(_applicationResourceDictionary);
            //}
            //_applicationResourceDictionary.BeginInit();
            //_applicationResourceDictionary.MergedDictionaries.Clear();

            //ResourceDictionary maindictionary = mainWindow.Resources;
            //if (maindictionary.Count() == 0)
            //{
            //}

            //ResourceDictionary windowResourceDictionary = mainWindow.Resources.MergedDictionaries[0];
            //windowResourceDictionary.BeginInit();
            //windowResourceDictionary.MergedDictionaries.Clear();

            //foreach (Uri uri in theme.ApplicationResources)
            //{
            //    _applicationResourceDictionary.MergedDictionaries.Add(new ResourceDictionary
            //    {
            //        Source = uri
            //    });
            //}

            //foreach (Uri uri in theme.MainWindowResources)
            //{
            //    windowResourceDictionary.MergedDictionaries.Add(new ResourceDictionary
            //    {
            //        Source = uri
            //    });
            //}

            //windowResourceDictionary.EndInit();
            //_applicationResourceDictionary.EndInit();
        //});


            // publish event
            _messenger.Publish(new ThemeChangeMessage(this, CurrentTheme.Name));

            if (applySetting)
            {
                Properties.Settings.Default.ThemeName = CurrentTheme.Name;
            }

            return true;
        }

    }
}
