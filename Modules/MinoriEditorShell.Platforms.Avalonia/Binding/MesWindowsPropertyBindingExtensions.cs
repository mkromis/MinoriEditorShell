namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public static class MesWindowsPropertyBindingExtensions
    {
        public static string BindVisible(this FrameworkElement frameworkElement)
            => MesWindowsPropertyBinding.FrameworkElement_Visible;

        public static string BindCollapsed(this FrameworkElement frameworkElement)
            => MesWindowsPropertyBinding.FrameworkElement_Collapsed;

        public static string BindHidden(this FrameworkElement frameworkElement)
            => MesWindowsPropertyBinding.FrameworkElement_Hidden;
    }
}
