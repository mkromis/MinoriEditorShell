using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MesToolBarBase : ToolBar
    {
        private Object _currentItem;

        protected override Boolean IsItemItsOwnContainerOverride(Object item)
        {
            _currentItem = item;
            return base.IsItemItsOwnContainerOverride(item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            switch(_currentItem)
            {
                case MesToolBarItemSeparator _:
                    return new Separator();
                case MesCommandToolBarItem _:
                    return CreateButton<MesCustomToggleButton>(ToggleButtonStyleKey, "ToolBarButton");
                default:
                    return base.GetContainerForItemOverride();
            }
        }

        private static T CreateButton<T>(Object baseStyleKey, String styleKey)
            where T : FrameworkElement, new()
        {
            T result = new T();
            result.SetResourceReference(MesDynamicStyle.BaseStyleProperty, baseStyleKey);
            result.SetResourceReference(MesDynamicStyle.DerivedStyleProperty, styleKey);
            return result;
        }
    }
}
