using Microsoft.Win32;
using MinoriDemo.RibbonWPF.DataClasses;
using MinoriDemo.RibbonWPF.Modules.Themes;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.Views
{
    /// <summary>
    /// Interaction logic for ToolSampleView.xaml
    /// </summary>
    [MvxContentPresentation]
    public partial class ThemeEditorView
    {
        public ThemeHelper _themeHelper;

        // TODO: I18n
        private const string _newKey = "Unnamed";

        public ThemeEditorView()
        {
            InitializeComponent();

            // Setup selection drop down item.
            _themeHelper = new ThemeHelper();
            ThemeSelection.ItemsSource = _themeHelper.GetAppDictionary().MergedDictionaries;
            ThemeSelection.SelectedItem = ThemeSelection.Items[0];
        }

        /// <summary>
        /// Add new item to list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            IDictionary<string, SolidColorBrush> brushes = _themeHelper.GetBrushes();
            if (brushes.Keys.Contains(_newKey))
            {
                MessageBox.Show($"{_newKey} already exists, please rename key before adding a new one", "Duplicate key");
                return;
            }
            brushes[_newKey] = new SolidColorBrush();
            _themeHelper.SetBrushes(brushes);

            UpdateList(brushes);
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeSelection == null)
            {
                MessageBox.Show("Select a theme to export");
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Resource Dictionary (*.xaml)|*.xaml",
                AddExtension = true,
                DefaultExt = ".xaml",
            };
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, _themeHelper.ExportString());
            }
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ThemeItem item)
            {
                IDictionary<string, SolidColorBrush> brushes = _themeHelper.GetBrushes();
                brushes.Remove(item.Key);
                _themeHelper.SetBrushes(brushes);

                UpdateList(brushes);
            }
        }

        private void RenameClick(object sender, RoutedEventArgs e)
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
                    string newKey = item.Key;
                    // where !old key
                    if (!string.IsNullOrEmpty(newKey) && newKey != item.OriginalKey)
                    {
                        // Get list
                        IDictionary<string, SolidColorBrush> brushes = _themeHelper.GetBrushes();

                        // add new key
                        if (brushes.ContainsKey(item.OriginalKey))
                        {
                            brushes[item.Key] = brushes[item.OriginalKey];
                            brushes.Remove(item.OriginalKey);
                        }
                        else
                        {
                            brushes[item.Key] = new SolidColorBrush();
                        }
                        _themeHelper.SetBrushes(brushes);

                        // update
                        UpdateList(brushes);
                    }
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            IDictionary<string, SolidColorBrush> brushes = _themeHelper.GetBrushes();
            if (!string.IsNullOrWhiteSpace(search.Text))
            {
                IEnumerable<KeyValuePair<string, SolidColorBrush>> select = brushes
                    .Where(x => x.Key.ToLower().Contains(search.Text.ToLower()));

                SortedDictionary<string, SolidColorBrush> result = new SortedDictionary<string, SolidColorBrush>();
                foreach (KeyValuePair<string, SolidColorBrush> item in select) { result[item.Key] = item.Value; }

                UpdateList(result);
            }
            else
            {
                UpdateList(brushes);
            }
        }

        private void search_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                Search_Click(sender, e);
            }
        }

        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            _themeHelper.CurrentThemeDictionary = ThemeSelection.SelectedItem as ResourceDictionary;
            if (_themeHelper.CurrentThemeDictionary != null)
            {
                UpdateList(_themeHelper.GetBrushes());
            }
        }

        /// <summary>
        /// Updates list from
        /// </summary>
        private void UpdateList(IDictionary<string, SolidColorBrush> brushes)
        {
            MainResourceList.ItemsSource =
                brushes.Select(x => new ThemeItem
                {
                    ThemeHelper = _themeHelper,
                    Key = x.Key,
                    Color = x.Value.Color,
                });
        }
    }
}