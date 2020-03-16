using System;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MinoriEditorShell.Platforms.Gtk.Core
{
    public class MesGtkSetup<TApplication> : MvxSetup 
        where TApplication : class, IMvxApplication, new()

    {
        public MesGtkSetup()
        {
        }

        protected override IMvxApplication CreateApp() => Mvx.IoCProvider.IoCConstruct<TApplication>();

        protected override IMvxViewDispatcher CreateViewDispatcher()
        {
            return null;
        }

        protected override IMvxViewsContainer CreateViewsContainer()
        {
            return null;
        }

        protected override IMvxNameMapping CreateViewToViewModelNaming()
        {
            return null;
        }
    }
}
