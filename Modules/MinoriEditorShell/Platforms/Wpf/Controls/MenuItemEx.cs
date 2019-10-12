using MinoriEditorShell.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MenuItemEx : System.Windows.Controls.MenuItem
	{
		private Object _currentItem;

		protected override Boolean IsItemItsOwnContainerOverride(Object item)
		{
			_currentItem = item;
			return base.IsItemItsOwnContainerOverride(item);
		}

        protected override DependencyObject GetContainerForItemOverride() => GetContainer(this, _currentItem);

        internal static DependencyObject GetContainer(FrameworkElement frameworkElement, Object item)
		{
		    if (item is MenuItemSeparator)
            {
                return new Separator();
            }

            const String styleKey = "MenuItem";

            MenuItemEx result = new MenuItemEx();
            result.SetResourceReference(DynamicStyle.BaseStyleProperty, typeof(MenuItem));
		    result.SetResourceReference(DynamicStyle.DerivedStyleProperty, styleKey);
		    return result;
		}
	}
}
