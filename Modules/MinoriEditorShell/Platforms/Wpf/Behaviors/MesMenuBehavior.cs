using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MinoriEditorShell.Commands;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Behaviors
{
    public class MesMenuBehavior : DependencyObject
    {
        public static readonly DependencyProperty UpdateCommandUiItemsProperty = DependencyProperty.RegisterAttached(
            "UpdateCommandUiItems", typeof(bool), typeof(MesMenuBehavior), new PropertyMetadata(false, OnUpdateCommandUiItemsChanged));

        public static bool GetUpdateCommandUiItems(DependencyObject control)
        {
            return (bool) control.GetValue(UpdateCommandUiItemsProperty);
        }

        public static void SetUpdateCommandUiItems(DependencyObject control, bool value)
        {
            control.SetValue(UpdateCommandUiItemsProperty, value);
        }

        private static void OnUpdateCommandUiItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menuItem = (MenuItem) d;
            menuItem.SubmenuOpened += OnSubmenuOpened;
            if (menuItem.IsSubmenuOpen)
                OnSubmenuOpened(menuItem, new RoutedEventArgs());
        }

        private static void OnSubmenuOpened(object sender, RoutedEventArgs e)
        {
            var commandRouter = Mvx.IoCProvider.Resolve<IMesCommandRouter>();
            var menuItem = (MenuItem) sender;
            foreach (var item in menuItem.Items.OfType<IMesCommandUiItem>().ToList())
                item.Update(commandRouter.GetCommandHandler(item.CommandDefinition));
        }
    }
}
