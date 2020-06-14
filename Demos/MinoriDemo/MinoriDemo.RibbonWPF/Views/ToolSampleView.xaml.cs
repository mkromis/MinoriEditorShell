using MinoriDemo.RibbonWPF.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinoriDemo.RibbonWPF.Views
{
    /// <summary>
    /// Interaction logic for ToolSampleView.xaml
    /// </summary>
    public partial class ToolSampleView
    {
        public ToolSampleView()
        {
            InitializeComponent();

            ResourceDictionary resources = Application.Current.Resources;
            ResourceDictionary appDictionary = resources.MergedDictionaries[0]; // should always be true if theme applied (App Theme)
            ResourceDictionary mainTheme = appDictionary.MergedDictionaries[0]; // MainTheme blue theme etc.
            FileName.Text = System.IO.Path.GetFileName(mainTheme.Source.ToString());
            Export.Click += Export_Click;

            List<ThemeItem> items = new List<ThemeItem>();
            foreach (Object key in mainTheme.Keys)
            {
                var value = mainTheme[key];
                //if (value is Color color)
                //{
                //    items.Add(new ThemeItem { 
                //        Key = key.ToString(), 
                //        Color = color,
                //        Resource = mainTheme,
                //    });
                //}
                if (value is SolidColorBrush brush)
                {
                    items.Add(new ThemeItem { 
                        Key = key.ToString(), 
                        Color = ((SolidColorBrush)brush).Color,
                        Resource = mainTheme,
                    });
                }
            }
            MainResourceList.ItemsSource = items.OrderBy(x=> x.Key);
        }

        private void Export_Click(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
