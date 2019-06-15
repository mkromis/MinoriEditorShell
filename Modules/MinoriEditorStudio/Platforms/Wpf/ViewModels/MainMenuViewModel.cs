using MinoriEditorStudio.Modules.MainMenu.Models;
using MinoriEditorStudio.Messages;
using MvvmCross.Plugin.Messenger;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System;

namespace MinoriEditorStudio.Modules.MainMenu.ViewModels
{
    [Export(typeof(IMenu))]
    public class MainMenuViewModel : MenuModel, IPartImportsSatisfiedNotification
	{
        private readonly IMenuBuilder _menuBuilder;
        private readonly IMvxMessenger _messenger;
        private bool _autoHide;

        [ImportingConstructor]
	    public MainMenuViewModel(IMenuBuilder menuBuilder, IMvxMessenger messenger)
	    {
            _menuBuilder = menuBuilder;
            _messenger = messenger;

            _autoHide = Properties.Settings.Default.AutoHideMainMenu;
            _messenger.Subscribe<SettingsChangedMessage>((x) =>
            {
                if (x.Name == "AutoHideMainMenu")
                {
                    AutoHide = (Boolean)x.Value;
                }
            });
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
