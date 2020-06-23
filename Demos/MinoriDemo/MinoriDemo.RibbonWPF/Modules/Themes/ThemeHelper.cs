using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.Modules.Themes
{
    class ThemeHelper
    {
        /// <summary>
        /// Gets all of the brushes in a dictionary format
        /// </summary>
        /// <returns></returns>
        public IDictionary<String, SolidColorBrush> GetBrushes()
        {
            ResourceDictionary resource = Application.Current.Resources.MergedDictionaries[0];
            ResourceDictionary appDict = resource.MergedDictionaries[0];
            SortedDictionary<String, SolidColorBrush> brushes = new SortedDictionary<String, SolidColorBrush>();
            foreach (Object item in appDict.Keys)
            {
                String newItem = item.ToString();
                Object current = resource[newItem];
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
        public void SetBrushes(IDictionary<String, SolidColorBrush> brushes)
        {
            ResourceDictionary resource = Application.Current.Resources.MergedDictionaries[0];
            // Reset visuals
            // Setup app style
            resource.BeginInit();
            resource.Clear();

            // Object type not known at this point
            foreach (String key in brushes.Keys)
            {
                resource[key] = brushes[key];
            }

            resource.EndInit();
        }

        public String ExportString(Boolean core, Boolean ribbon)
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

            IEnumerable<String> keys = GetBrushes().Keys.OrderBy(x => x);
            if (core && !ribbon)
            {
                keys = keys.Where(x => !x.StartsWith("Fluent"));
            }
            if (!core && ribbon)
            {
                keys = keys.Where(x => x.StartsWith("Fluent"));
            }

            ResourceDictionary resource = Application.Current.Resources.MergedDictionaries[0];
            foreach (String key in keys)
            {
                if (resource[key] is SolidColorBrush solidColor)
                {
                    export.AppendLine($"    <SolidColorBrush x:Key=\"{key}\" Color=\"{solidColor.Color}\"  options:Freeze=\"True\" />");
                }
            }

            export.AppendLine("    <!-- End SolidColorBrush Export -->");
            export.AppendLine("</ResourceDictionary>");
            return export.ToString();
        }

    }
}
