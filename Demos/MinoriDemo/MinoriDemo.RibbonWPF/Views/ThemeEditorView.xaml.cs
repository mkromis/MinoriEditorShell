using AvalonDock.Themes;
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
        // TODO: I18n
        const String _newKey = "Unnamed";
        public ThemeHelper _themeHelper;

        public ThemeEditorView()
        {
            InitializeComponent();

            // Setup selection drop down item.
            _themeHelper = new ThemeHelper();
            ThemeSelection.ItemsSource = _themeHelper.GetAppDictionary().MergedDictionaries;
            ThemeSelection.SelectedItem = ThemeSelection.Items[0];
        }

        /// <summary>
        /// Updates list from 
        /// </summary>
        private void UpdateList(IDictionary<String, SolidColorBrush> brushes)
        {
            MainResourceList.ItemsSource =
                brushes.Select(x => new ThemeItem
                {
                    ThemeHelper = _themeHelper,
                    Key = x.Key,
                    Color = x.Value.Color,
                });
        }

        private void Export_Click(Object sender, RoutedEventArgs e)
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

        private void Search_Click(Object sender, RoutedEventArgs e)
        {
            IDictionary<String, SolidColorBrush> brushes = _themeHelper.GetBrushes();
            if (!String.IsNullOrWhiteSpace(search.Text))
            {
                IEnumerable<KeyValuePair<String, SolidColorBrush>> select = brushes
                    .Where(x => x.Key.ToLower().Contains(search.Text.ToLower()));

                SortedDictionary<String, SolidColorBrush> result = new SortedDictionary<String, SolidColorBrush>();
                foreach (KeyValuePair<String, SolidColorBrush> item in select) { result[item.Key] = item.Value; }

                UpdateList(result);
            }
            else
            {
                UpdateList(brushes);
            }
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

            UpdateList(brushes);
        }

        private void RemoveClick(Object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ThemeItem item)
            {
                IDictionary<String, SolidColorBrush> brushes = _themeHelper.GetBrushes();
                brushes.Remove(item.Key);
                _themeHelper.SetBrushes(brushes);

                UpdateList(brushes);
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
                        if (brushes.ContainsKey(item.OriginalKey))
                        {
                            brushes[item.Key] = brushes[item.OriginalKey];
                            brushes.Remove(item.OriginalKey);
                        } else
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

        private void search_KeyDown(Object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                Search_Click(sender, e);
            }
        }

        private void ThemeChanged(Object sender, SelectionChangedEventArgs e)
        {
            _themeHelper.CurrentThemeDictionary = ThemeSelection.SelectedItem as ResourceDictionary;
            if (_themeHelper.CurrentThemeDictionary != null)
            {
                UpdateList(_themeHelper.GetBrushes());
            }
        }
    }
}
