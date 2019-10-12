using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;

namespace MinoriEditorShell.Services
{
    public abstract class WindowBase : MvxNavigationViewModel, IWindow
	{
        protected WindowBase(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public String DisplayName { get; set; }
	}
}
