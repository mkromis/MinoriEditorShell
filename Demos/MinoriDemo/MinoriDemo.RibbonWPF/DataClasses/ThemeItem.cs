using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.PeerToPeer.Collaboration;
using System.Windows;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.DataClasses
{
    class ThemeItem : MvxNotifyPropertyChanged
    {
        private Color _color;
        private String oldKey;
        private String key;
        private Boolean canEdit = false;
        private String modeText = "Edit";

        public String ModeText { 
            get => modeText; 
            set => SetProperty(ref modeText, value); 
        }
        public Boolean CanEdit
        {
            get => canEdit;
            set => SetProperty(ref canEdit, value);
        }
        public ResourceDictionary Resource { get; internal set; }
        public String Key
        {
            get => key;
            set => SetProperty(ref key, value);
        }
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;

                if (Resource == null)
                {
                    return;
                }

                Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>()
                .ExecuteOnMainThreadAsync(() =>
                {
                    Dictionary<String, SolidColorBrush> brushes = ConvertToDictionary();

                    SolidColorBrush brush = new SolidColorBrush(value);
                    brushes[Key] = brush;

                    ConvertToResource(brushes);
                });
            }
        }

        public IMvxCommand EditCommand => new MvxCommand(() => RenameValues());

        public void Create()
        {
            Dictionary<String, SolidColorBrush> brushes = ConvertToDictionary();

            SolidColorBrush brush = new SolidColorBrush(Color);
            brushes[Key] = brush;

            ConvertToResource(brushes);
        }

        private void RenameValues()
        {
            if (!CanEdit)
            {
                oldKey = Key;
                CanEdit = true;
                ModeText = "Done";
            }
            else
            {
                CanEdit = false;
                ModeText = "Edit";

                // Set new name here
                String newKey = Key;
                // where !old key 
                if (!String.IsNullOrEmpty(newKey) && newKey != oldKey)
                {
                    // Get list
                    Dictionary<String, SolidColorBrush> brushes = ConvertToDictionary();
                    
                    // add new key
                    brushes[newKey] = brushes[oldKey];
                    brushes.Remove(oldKey);

                    // update
                    ConvertToResource(brushes);
                }
            }
        }

        /// <summary>
        /// Convert dictionary to resources
        /// </summary>
        /// <param name="brushes"></param>
        private void ConvertToResource(Dictionary<String, SolidColorBrush> brushes)
        {
            // Reset visuals
            // Setup app style
            ResourceDictionary appTheme = Application.Current.Resources.MergedDictionaries[0];
            appTheme.BeginInit();

            appTheme.Clear();

            // Object type not known at this point
            foreach (String key2 in brushes.Keys)
            {
                Resource[key2] = brushes[key2];
            }

            appTheme.EndInit();
        }

        /// <summary>
        /// Convert brushes into dictionary. This will convert old key to newKey
        /// </summary>
        /// <returns></returns>
        private Dictionary<String, SolidColorBrush> ConvertToDictionary()
        {
            Dictionary<String, SolidColorBrush> brushes = new Dictionary<String, SolidColorBrush>();
            foreach (String item in Resource.Keys)
            {
                Object current = Resource[item];
                if (current is SolidColorBrush brush)
                {
                    brushes[item] = brush;
                }
            }
            return brushes;
        }
    }
}
