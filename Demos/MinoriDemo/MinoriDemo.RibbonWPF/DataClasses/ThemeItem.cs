using MinoriDemo.RibbonWPF.Modules.Themes;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.DataClasses
{
    class ThemeItem : MvxNotifyPropertyChanged
    {
        private Color _color;
        private String _key;
        private Boolean _canEdit = false;
        private String _modeText = "Edit";

        public ThemeHelper ThemeHelper { get; set; }

        public String ModeText { 
            get => _modeText; 
            set => SetProperty(ref _modeText, value); 
        }
        public Boolean CanEdit
        {
            get => _canEdit;
            set
            {
                if(SetProperty(ref _canEdit, value))
                {
                    ModeText = value ? "Done" : "Edit";
                }
            }
        }
        public String Key
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
                    IDictionary<String, SolidColorBrush> brushes = ThemeHelper.GetBrushes();
                    brushes[Key] = new SolidColorBrush(value);
                    ThemeHelper.SetBrushes(brushes);
                });
            }
        }

        public String OriginalKey { get; internal set; }
    }
}
