using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.Menus;
using System;
using System.Globalization;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Models
{
    public class MesTextMenuItem : MesStandardMenuItem
	{
	    private readonly MesMenuDefinitionBase _menuDefinition;

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

        public MesTextMenuItem(MesMenuDefinitionBase menuDefinition)
        {
            _menuDefinition = menuDefinition;
        }
	}
}
