using MinoriDemo.RibbonWPF.Modules.Themes;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.DataClasses
{
    internal class ThemeItem : MvxNotifyPropertyChanged
    {
        private Color _color;
        private string _key;
        private bool _canEdit = false;
        private string _modeText = "Edit";

        public ThemeHelper ThemeHelper { get; set; }

        public string ModeText
        {
            get => _modeText;
            set => SetProperty(ref _modeText, value);
        }

        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                if (SetProperty(ref _canEdit, value))
                {
                    ModeText = value ? "Done" : "Edit";
                }
            }
        }

        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;

                Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>()
                .ExecuteOnMainThreadAsync(() =>
                {
                    IDictionary<string, SolidColorBrush> brushes = ThemeHelper.GetBrushes();
                    brushes[Key] = new SolidColorBrush(value);
                    ThemeHelper.SetBrushes(brushes);
                });
            }
        }

        public string OriginalKey { get; internal set; }
    }
}