using System.ComponentModel;
using System.ComponentModel.Composition;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.MainMenu.Models;
using ExtensionMethods = MinoriEditorStudio.Framework.Services.ExtensionMethods;

namespace MinoriEditorStudio.Modules.MainMenu.ViewModels
{
	[Export(typeof(IMenu))]
    public class MainMenuViewModel : MenuModel, IPartImportsSatisfiedNotification
	{
        private readonly IMenuBuilder _menuBuilder;

	    private bool _autoHide;

	    private readonly SettingsPropertyChangedEventManager<Properties.Settings> _settingsEventManager =
	        new SettingsPropertyChangedEventManager<Properties.Settings>(Properties.Settings.Default);

        [ImportingConstructor]
	    public MainMenuViewModel(IMenuBuilder menuBuilder)
	    {
            _menuBuilder = menuBuilder;
            _autoHide = Properties.Settings.Default.AutoHideMainMenu;
            _settingsEventManager.AddListener(s => s.AutoHideMainMenu, value => { AutoHide = value; });
		}

	    public bool AutoHide
	    {
	        get { return _autoHide; }
	        private set
	        {
	            if (_autoHide == value)
	                return;

	            _autoHide = value;

	            OnPropertyChanged(new PropertyChangedEventArgs(nameof(AutoHide)));
	        }
	    }

	    void IPartImportsSatisfiedNotification.OnImportsSatisfied()
	    {
	        _menuBuilder.BuildMenuBar(MenuDefinitions.MainMenuBar, this);
	    }
	}
}
