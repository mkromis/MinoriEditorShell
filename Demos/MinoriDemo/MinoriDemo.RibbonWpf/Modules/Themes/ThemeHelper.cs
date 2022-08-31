using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.Modules.Themes
{
    public class ThemeHelper
    {
        public ResourceDictionary GetAppDictionary() => Application.Current.Resources.MergedDictionaries[0];

        public ResourceDictionary CurrentThemeDictionary { get; set; }

        /// <summary>
        /// Gets all of the brushes in a dictionary format
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, SolidColorBrush> GetBrushes()
        {
            SortedDictionary<string, SolidColorBrush> results = new SortedDictionary<string, SolidColorBrush>();

            // Get theme dict
            ResourceDictionary theme = CurrentThemeDictionary;

            // if theme is not selected yet, don't process
            if (theme != null)
            {
                foreach (object item in theme.Keys)
                {
                    // Construct solid color brush
                    string newItem = item.ToString();
                    object current = theme[newItem];
                    if (current is SolidColorBrush brush)
                    {
                        results[newItem] = brush;
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Convert dictionary to resources
        /// </summary>
        /// <param name="brushes"></param>
        public void SetBrushes(IDictionary<string, SolidColorBrush> brushes)
        {
            ResourceDictionary appDict = GetAppDictionary();
            ResourceDictionary themeDict = CurrentThemeDictionary;

            // Reset visuals
            // Setup app style
            appDict.BeginInit();
            themeDict.Clear();

            // Object type not known at this point
            foreach (string key in brushes.Keys)
            {
                themeDict[key] = brushes[key];
            }

            appDict.EndInit();
        }

        public string ExportString()
        {
            ResourceDictionary theme = CurrentThemeDictionary;
            if (theme == null)
            {
                throw new InvalidOperationException("CurrentThemeDictionary");
            }

            StringBuilder export = new StringBuilder();
            export.AppendLine("<ResourceDictionary");
            export.AppendLine("    xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            export.AppendLine("    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
            export.AppendLine("    xmlns:System=\"clr-namespace:System;assembly=System.Runtime\"");
            export.AppendLine("    xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"");
            export.AppendLine("    xmlns:options=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation/options\"");
            export.AppendLine("    mc:Ignorable=\"options\">");

            // Export dictionary
            export.AppendLine("    <ResourceDictionary.MergedDictionaries>");
            foreach (ResourceDictionary dictionary in theme.MergedDictionaries)
            {
                export.AppendLine($"        <ResourceDictionary Source=\"{dictionary.Source}\" />");
            }
            export.AppendLine("    </ResourceDictionary.MergedDictionaries>");

            // Export colors
            export.AppendLine("    <!-- Begin SolidColorBrush Export -->");
            IDictionary<string, SolidColorBrush> brushes = GetBrushes();
            foreach (string key in brushes.Keys)
            {
                export.AppendLine($"    <SolidColorBrush x:Key=\"{key}\" Color=\"{brushes[key].Color}\"  options:Freeze=\"True\" />");
            }
            export.AppendLine("    <!-- End SolidColorBrush Export -->");

            // End
            export.AppendLine("</ResourceDictionary>");
            return export.ToString();
        }
    }
}