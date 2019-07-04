using MinoriEditorStudio.Messages;
using MvvmCross.Plugin.Messenger;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.Models;
using MinoriEditorStudio.Platforms.Wpf.Services;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    [Export(typeof(IMenu))]
    public class MainMenuViewModel : MenuModel, IPartImportsSatisfiedNotification
	{
        private readonly IMenuBuilder _menuBuilder;
        private readonly IMvxMessenger _messenger;
        private Boolean _autoHide;

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

	    public Boolean AutoHide
        {
            get => _autoHide;
            private set
            {
                if (_autoHide == value)
                {
                    return;
                }

                _autoHide = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AutoHide)));
            }
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
	    {
	        _menuBuilder.BuildMenuBar(MenuDefinitionCollection.MainMenuBar, this);
	    }
	}
}
