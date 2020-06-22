using Microsoft.Win32;
using MinoriDemo.RibbonWPF.DataClasses;
using MinoriDemo.RibbonWPF.Modules.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.Views
{
    /// <summary>
    /// Interaction logic for ToolSampleView.xaml
    /// </summary>
    public partial class ThemeEditorView
    {
        const String _newKey = "Un-named";

        private ThemeHelper _themeHelper { get; }

        public ThemeEditorView()
        {
            InitializeComponent();

            ResourceDictionary resources = Application.Current.Resources;

            // should always be true if theme applied (App Theme)
            ResourceDictionary appDictionary = resources.MergedDictionaries[0];

            _themeHelper = new ThemeHelper
            {
                Dictionary = appDictionary.MergedDictionaries[0],
            };

            FileName.Text = Path.GetFileName(_themeHelper.Dictionary.Source.ToString());

            UpdateList();
        }

        /// <summary>
        /// Updates list from 
        /// </summary>
        private void UpdateList()
        {
            MainResourceList.ItemsSource =
                _themeHelper
                    .GetBrushes()
                    .Select(x => new ThemeItem
                    {
                        Key = x.Key,
                        Color = x.Value.Color,
                        ThemeHelper = _themeHelper,
                    });
        }

        private void Export_Click(Object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Resource Dictionary (*.xaml)|*.xaml",
                AddExtension = true,
                DefaultExt = ".xaml",
            };
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, _themeHelper.ExportString(true, true));
            }
        }

        private void Search_Click(Object sender, RoutedEventArgs e)
        {
            //if (!String.IsNullOrWhiteSpace(search.Text))
            //{
            //    IEnumerable<String> keys = GetBrushes().Where(x => x.ToLower().Contains(search.Text.ToLower()));
            //    UpdateList(keys);
            //} else
            //{
            //    UpdateList(GetBrushes());
            //}
        }

        /// <summary>
        /// Add new item to list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(Object sender, RoutedEventArgs e)
        {
            IDictionary<String, SolidColorBrush> brushes = _themeHelper.GetBrushes();
            if (brushes.Keys.Contains(_newKey))
            {
                MessageBox.Show($"{_newKey} already exists, please rename key before adding a new one", "Duplicate key");
                return;
            }
            brushes[_newKey] = new SolidColorBrush();
            _themeHelper.SetBrushes(brushes);

            UpdateList();
        }

        private void RemoveClick(Object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ThemeItem item)
            {
                IDictionary<String, SolidColorBrush> brushes = _themeHelper.GetBrushes();
                brushes.Remove(item.Key);
                _themeHelper.SetBrushes(brushes);

                UpdateList();
            }
        }

        private void RenameClick(Object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ThemeItem item)
            {
                if (!item.CanEdit)
                {
                    item.OriginalKey = item.Key;
                    item.CanEdit = true;
                }
                else
                {
                    item.CanEdit = false;

                    // Set new name here
                    String newKey = item.Key;
                    // where !old key 
                    if (!String.IsNullOrEmpty(newKey) && newKey != item.OriginalKey)
                    {
                        // Get list
                        IDictionary<String, SolidColorBrush> brushes = _themeHelper.GetBrushes();

                        // add new key
                        brushes[item.Key] = brushes[item.OriginalKey];
                        brushes.Remove(item.OriginalKey);
                        _themeHelper.SetBrushes(brushes);

                        // update
                        UpdateList();
                    }
                }
            }
        }
    }
}
