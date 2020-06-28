using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.Modules.Themes
{
    public static class ThemeHelper
    {
        public static ResourceDictionary GetAppDictionary ()
        {
            return Application.Current.Resources.MergedDictionaries[0];
        }

        public static ResourceDictionary CurrentThemeDictionary { get; set; }

        /// <summary>
        /// Gets all of the brushes in a dictionary format
        /// </summary>
        /// <returns></returns>
        public static IDictionary<String, SolidColorBrush> GetBrushes()
        {
            SortedDictionary<String, SolidColorBrush> results = new SortedDictionary<String, SolidColorBrush>();
            
            // Get theme dict
            var theme = CurrentThemeDictionary;

            // if theme is not selected yet, don't process
            if (theme != null)
            {
                foreach (Object item in theme.Keys)
                {
                    // Construct solid color brush
                    String newItem = item.ToString();
                    Object current = theme[newItem];
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
        public static void SetBrushes(IDictionary<String, SolidColorBrush> brushes)
        {
            ResourceDictionary appDict = GetAppDictionary();
            ResourceDictionary themeDict = CurrentThemeDictionary;

            // Reset visuals
            // Setup app style
            appDict.BeginInit();
            themeDict.Clear();

            // Object type not known at this point
            foreach (String key in brushes.Keys)
            {
                themeDict[key] = brushes[key];
            }

            appDict.EndInit();
        }

        public static String ExportString()
        {
            var theme = CurrentThemeDictionary;
            if (theme == null) throw new ArgumentNullException("CurrentThemeDictionary");

            Boolean generic = false; // <-- Want this to be generic but its style is based on a static class
            StringBuilder export = new StringBuilder();
            export.AppendLine("<ResourceDictionary");
            export.AppendLine("    xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            export.AppendLine("    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
            export.AppendLine("    xmlns:System=\"clr-namespace:System;assembly=System.Runtime\"");
            export.AppendLine("    xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"");
            export.AppendLine("    xmlns:options=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation/options\"");
            export.AppendLine("    mc:Ignorable=\"options\">");
            export.AppendLine("    <ResourceDictionary.MergedDictionaries>");
            foreach (var dictionary in theme.MergedDictionaries)
            {

                //export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml\" />");
            }

            export.AppendLine("    </ResourceDictionary.MergedDictionaries>");
            export.AppendLine("");
            export.AppendLine("    <!-- Begin SolidColorBrush Export -->");

            IDictionary<String, SolidColorBrush> brushes = GetBrushes();
            foreach (String key in brushes.Keys)
            {
                export.AppendLine($"    <SolidColorBrush x:Key=\"{key}\" Color=\"{brushes[key].Color}\"  options:Freeze=\"True\" />");
            }

            export.AppendLine("    <!-- End SolidColorBrush Export -->");
            export.AppendLine("</ResourceDictionary>");
            return export.ToString();
        }
    }
}
