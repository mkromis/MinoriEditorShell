using System.Windows;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public static class MesVisualTreeUtility
    {
        public static T FindParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;

            return FindParent<T>(parentObject);
        }
    }
}