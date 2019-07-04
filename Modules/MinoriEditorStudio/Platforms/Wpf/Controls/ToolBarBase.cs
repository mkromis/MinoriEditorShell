using MinoriEditorStudio.Models;
using MinoriEditorStudio.Platforms.Wpf.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorStudio.Platforms.Wpf.Controls
{
    public class ToolBarBase : ToolBar
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
                case ToolBarItemSeparator _:
                    return new Separator();
                case CommandToolBarItem _:
                    return CreateButton<CustomToggleButton>(ToggleButtonStyleKey, "ToolBarButton");
                default:
                    return base.GetContainerForItemOverride();
            }
        }

        private static T CreateButton<T>(Object baseStyleKey, String styleKey)
            where T : FrameworkElement, new()
        {
            T result = new T();
            result.SetResourceReference(DynamicStyle.BaseStyleProperty, baseStyleKey);
            result.SetResourceReference(DynamicStyle.DerivedStyleProperty, styleKey);
            return result;
        }
    }
}
