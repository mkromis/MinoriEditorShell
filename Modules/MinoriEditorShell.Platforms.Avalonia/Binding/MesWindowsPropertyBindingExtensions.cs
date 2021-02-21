using Avalonia;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public static class MesWindowsPropertyBindingExtensions
    {
        public static string BindVisible(this AvaloniaObject frameworkElement)
            => MesWindowsPropertyBinding.FrameworkElement_Visible;

        public static string BindCollapsed(this AvaloniaObject frameworkElement)
            => MesWindowsPropertyBinding.FrameworkElement_Collapsed;

        public static string BindHidden(this AvaloniaObject frameworkElement)
            => MesWindowsPropertyBinding.FrameworkElement_Hidden;
    }
}
