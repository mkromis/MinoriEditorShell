using MvvmCross;
using MvvmCross.Base;
using System;
using System.Windows;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.DataClasses
{
    class ThemeItem
    {
        private Color _color;

        public String Key { get; set; }
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
                    // Setup app style
                    ResourceDictionary appTheme = Application.Current.Resources.MergedDictionaries[0];
                    appTheme.BeginInit();

                    // Object type not known at this point
                    Object item = Resource[Key];
                    if (item is Color)
                    {
                        Resource[Key] = value;
                    }

                    if (item is SolidColorBrush)
                    {
                        SolidColorBrush brush = new SolidColorBrush(value);
                        Resource[Key] = brush;
                    }

                    appTheme.EndInit();
                });
            }
        }
        public ResourceDictionary Resource { get; internal set; }
    }
}
