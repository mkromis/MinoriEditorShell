using MinoriEditorShell.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MesMenuItemEx : System.Windows.Controls.MenuItem
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
		    if (item is MesMenuItemSeparator)
            {
                return new Separator();
            }

            const String styleKey = "MenuItem";

            MesMenuItemEx result = new MesMenuItemEx();
            result.SetResourceReference(MesDynamicStyle.BaseStyleProperty, typeof(MenuItem));
		    result.SetResourceReference(MesDynamicStyle.DerivedStyleProperty, styleKey);
		    return result;
		}
	}
}
