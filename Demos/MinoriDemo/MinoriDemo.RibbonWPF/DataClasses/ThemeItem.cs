using MvvmCross;
using MvvmCross.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    String test = Resource.ToString();
                    var test2 = Application.Current.MainWindow.Resources;

                    // Setup app style
                    ResourceDictionary appTheme =
                        Application.Current.Resources.MergedDictionaries.Count > 0
                        ? Application.Current.Resources.MergedDictionaries[0] : null;

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
