using MinoriEditorShell.Messages;
using MvvmCross.Plugin.Messenger;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System;
using MinoriEditorShell.Services;
using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.Services;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    [Export(typeof(IMesMenu))]
    public class MesMainMenuViewModel : MesMenuModel, IPartImportsSatisfiedNotification
	{
        private readonly IMesMenuBuilder _menuBuilder;
        private readonly IMvxMessenger _messenger;
        private Boolean _autoHide;

        [ImportingConstructor]
	    public MesMainMenuViewModel(IMesMenuBuilder menuBuilder, IMvxMessenger messenger)
	    {
            _menuBuilder = menuBuilder;
            _messenger = messenger;

            _autoHide = Properties.Settings.Default.AutoHideMainMenu;
            _messenger.Subscribe<MesSettingsChangedMessage>((x) =>
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
            throw new NotImplementedException();
	        //_menuBuilder.BuildMenuBar(MenuDefinitionCollection.MainMenuBar, this);
	    }
	}
}
