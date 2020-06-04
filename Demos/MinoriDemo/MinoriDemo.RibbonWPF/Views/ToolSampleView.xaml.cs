﻿using MinoriDemo.RibbonWPF.DataClasses;
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

            var resources = Application.Current.Resources;
            var appDictionary = resources.MergedDictionaries[0]; // should always be true if theme applied
            var mainTheme = appDictionary.MergedDictionaries[0]; // MainTheme

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
    }
}
