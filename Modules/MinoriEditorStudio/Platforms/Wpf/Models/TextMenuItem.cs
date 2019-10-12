using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System;
using System.Globalization;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Models
{
    public class TextMenuItem : StandardMenuItem
	{
	    private readonly MenuDefinitionBase _menuDefinition;

        public override String Text => _menuDefinition.Text;

        public override Uri IconSource => _menuDefinition.IconSource;

        public override String InputGestureText
		{
			get
			{
                return _menuDefinition.KeyGesture == null
					? String.Empty
                    : _menuDefinition.KeyGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);
			}
		}

        public override ICommand Command => null;

        public override Boolean IsChecked => false;

        public override Boolean IsVisible => true;

        public TextMenuItem(MenuDefinitionBase menuDefinition)
        {
            _menuDefinition = menuDefinition;
        }
	}
}
