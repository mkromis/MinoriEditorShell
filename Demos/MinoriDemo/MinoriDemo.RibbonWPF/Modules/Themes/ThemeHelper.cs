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
        public static ResourceDictionary GetThemeDictionary ()
        {
            ResourceDictionary resource = Application.Current.Resources.MergedDictionaries[0];
            ResourceDictionary appDict = resource.MergedDictionaries[0];
            return appDict;
        }

        /// <summary>
        /// Gets all of the brushes in a dictionary format
        /// </summary>
        /// <returns></returns>
        public static IDictionary<String, SolidColorBrush> GetBrushes()
        {
            // Get theme dict
            var theme = GetThemeDictionary();

            SortedDictionary<String, SolidColorBrush> brushes = new SortedDictionary<String, SolidColorBrush>();
            foreach (Object item in theme.Keys)
            {
                String newItem = item.ToString();
                Object current = theme[newItem]; // <-- Is this right?
                if (current is SolidColorBrush brush)
                {
                    brushes[newItem] = brush;
                }
            }
            return brushes;
        }

        /// <summary>
        /// Convert dictionary to resources
        /// </summary>
        /// <param name="brushes"></param>
        public static void SetBrushes(IDictionary<String, SolidColorBrush> brushes)
        {
            var appDict = GetThemeDictionary();

            // Reset visuals
            // Setup app style
            appDict.BeginInit();
            appDict.Clear();

            // Object type not known at this point
            foreach (String key in brushes.Keys)
            {
                appDict[key] = brushes[key];
            }

            appDict.EndInit();
        }

        public static String ExportString(Boolean core, Boolean ribbon)
        {
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
            if (core)
            {
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml\" />");
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml\" />");
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/MahApps.Metro;component/Styles/VS/Controls.xaml\" />");
            }
            if (generic) 
            { 
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/AvalonDock.Themes.VS2013;component/Themes/Generic.xaml\" />");
            } else
            {
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/AvalonDock.Themes.VS2013;component/BlueTheme.xaml\" />"); 
            }
            if (ribbon)
            {
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/Fluent;Component/Themes/Generic.xaml\" />");
            }

            //if (colorpicker)
            {
                export.AppendLine("        <ResourceDictionary Source=\"pack://application:,,,/ColorPickerLib;component/Themes/Generic.xaml\" />");
            }
            export.AppendLine("    </ResourceDictionary.MergedDictionaries>");
            export.AppendLine("");
            export.AppendLine("    <!-- Begin SolidColorBrush Export -->");

            IDictionary<String, SolidColorBrush> brushes = GetBrushes();

            IEnumerable<String> keys = brushes.Keys.OrderBy(x => x);
            if (core && !ribbon)
            {
                keys = keys.Where(x => !x.StartsWith("Fluent"));
            }
            if (!core && ribbon)
            {
                keys = keys.Where(x => x.StartsWith("Fluent"));
            }

            foreach (String key in keys)
            {
                export.AppendLine($"    <SolidColorBrush x:Key=\"{key}\" Color=\"{brushes[key].Color}\"  options:Freeze=\"True\" />");
            }

            export.AppendLine("    <!-- End SolidColorBrush Export -->");
            export.AppendLine("</ResourceDictionary>");
            return export.ToString();
        }
    }
}
