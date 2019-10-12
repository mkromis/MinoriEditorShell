using System;
using System.Windows;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
	public class MenuEx : System.Windows.Controls.Menu
	{
	    public static readonly DependencyProperty AutoHideProperty = DependencyProperty.Register(
	        "AutoHide", typeof (Boolean), typeof (MenuEx), new PropertyMetadata(default(Boolean), AutoHidePropertyChangedCallback));

		private Object _currentItem;

        public Boolean AutoHide
        {
            get => (Boolean)GetValue(AutoHideProperty);
            set => SetValue(AutoHideProperty, value);
        }

        protected override Boolean IsItemItsOwnContainerOverride(object item)
		{
			_currentItem = item;
			return base.IsItemItsOwnContainerOverride(item);
		}

        protected override DependencyObject GetContainerForItemOverride() => MenuItemEx.GetContainer(this, _currentItem);

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
	    {
	        base.OnGotKeyboardFocus(e);
	        UpdateVisibility();
	    }

	    protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
	    {
	        base.OnLostKeyboardFocus(e);
            UpdateVisibility();
	    }

	    protected override void OnLostFocus(RoutedEventArgs e)
	    {
	        base.OnLostFocus(e);
            UpdateVisibility();
	    }

	    private static void AutoHidePropertyChangedCallback(DependencyObject dependencyObject,
	        DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
	    {
            MenuEx menu = (MenuEx) dependencyObject;
            menu.UpdateVisibility();
	    }

	    private void UpdateVisibility()
        {
            if (!AutoHide)
	        {
                Height = Double.NaN;
                return;
	        }

	        Height = IsKeyboardFocused || IsFocused || IsKeyboardFocusWithin ? Double.NaN : 0;
        }
	}
}
