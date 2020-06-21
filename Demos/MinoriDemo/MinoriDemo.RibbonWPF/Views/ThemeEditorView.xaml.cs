using Microsoft.Win32;
using MinoriDemo.RibbonWPF.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.Views
{
    /// <summary>
    /// Interaction logic for ToolSampleView.xaml
    /// </summary>
    public partial class ThemeEditorView
    {
        ResourceDictionary _mainTheme;
        String _newKey = "Un-named";

        public ThemeEditorView()
        {
            InitializeComponent();

            ResourceDictionary resources = Application.Current.Resources;
            ResourceDictionary appDictionary = resources.MergedDictionaries[0]; // should always be true if theme applied (App Theme)
            _mainTheme = appDictionary.MergedDictionaries[0]; // MainTheme blue theme etc.
            FileName.Text = Path.GetFileName(_mainTheme.Source.ToString());

            IEnumerable<String> keys = GetKeys();
            UpdateList(keys);
        }

        private void UpdateList(IEnumerable<String> keys)
        {
            List<ThemeItem> items = new List<ThemeItem>();
            foreach (String key in keys)
            {
                Object value = _mainTheme[key];
                if (value is null)
                {
                    ThemeItem item = new ThemeItem
                    {
                        Key = _newKey,
                        Color = new Color(),
                        Resource = _mainTheme,
                    };
                    item.Create();
                    items.Add(item);
                }
                if (value is SolidColorBrush brush)
                {
                    items.Add(new ThemeItem
                    {
                        Key = key.ToString(),
                        Color = brush.Color,
                        Resource = _mainTheme,
                    });
                }
            }

            MainResourceList.ItemsSource = items.OrderBy(x => x.Key);
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
                File.WriteAllText(saveFile.FileName, ExportString(true,true));
            }
        }

        private String ExportString(Boolean core, Boolean ribbon)
        {
            StringBuilder export = new StringBuilder();
            export.AppendLine("<ResourceDictionary");
            export.AppendLine("    xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            export.AppendLine("    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
            export.AppendLine("    xmlns:System=\"clr-namespace:System;assembly=System.Runtime\"");
            export.AppendLine("    xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"");
            export.AppendLine("    xmlns:options=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation/options\"");
            export.AppendLine("    mc:Ignorable=\"options\">");
            export.AppendLine("    <ResourceDictionary.MergedDictionaries>");
            if (core)
            {
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml\" />");
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml\" />");
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/MahApps.Metro;component/Styles/VS/Controls.xaml\" />");
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/AvalonDock.Themes.VS2013;component/Themes/Generic.xaml\" />");
            }
            if (ribbon)
            {
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/Fluent;Component/Themes/Generic.xaml\" />");
            }
            export.AppendLine("    </ResourceDictionary.MergedDictionaries>");
            export.AppendLine("");
            export.AppendLine("    <!-- Begin SolidColorBrush Export -->");

            IEnumerable<String> keys = GetKeys();
            if (core && !ribbon)
            {
                keys = keys.Where(x => !x.StartsWith("Fluent"));
            }
            if (!core && ribbon)
            {
                keys = keys.Where(x => x.StartsWith("Fluent"));
            }

            foreach (String key in keys.OrderBy(x => x))
            {
                switch (_mainTheme[key])
                {
                    case SolidColorBrush solidColor:
                        export.AppendLine($"    <SolidColorBrush x:Key=\"{key}\" Color=\"{solidColor.Color}\"  options:Freeze=\"True\" />");
                        break;
                    default:
                        break;
                }
            }

            export.AppendLine("    <!-- End SolidColorBrush Export -->");
            export.AppendLine("</ResourceDictionary>");
            return export.ToString();
        }

        private IEnumerable<String> GetKeys()
        {
            List<String> keys = new List<String>();
            foreach (Object key in _mainTheme.Keys)
            {
                keys.Add(key.ToString());
            }

            return keys;
        }

        private void Search_Click(Object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(search.Text))
            {
                IEnumerable<String> keys = GetKeys().Where(x => x.ToLower().Contains(search.Text.ToLower()));
                UpdateList(keys);
            } else
            {
                UpdateList(GetKeys());
            }
        }

        /// <summary>
        /// Add new item to list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(Object sender, RoutedEventArgs e)
        {
            List<String> keys = GetKeys().ToList();
            if (keys.Contains(_newKey))
            {
                MessageBox.Show($"{_newKey} already exists, please rename key before adding a new one", "Duplicate key");
                return;
            }
            keys.Add(_newKey);
            UpdateList(keys);
        }
    }
}
