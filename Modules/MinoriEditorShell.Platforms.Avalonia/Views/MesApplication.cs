using Avalonia;
using Avalonia.Threading;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public abstract class MesApplication : Application
    {
        public MesApplication() : base()
        {
            RegisterSetup();
        }

        public virtual void ApplicationInitialized()
        {
            //if (MainWindow == null) return;
            //MvxWpfSetupSingleton.EnsureSingletonAvailable(Dispatcher, MainWindow).EnsureInitialized();

            RunAppStart();
        }

        protected virtual void RunAppStart(object hint = null)
        {
            if (Mvx.IoCProvider.TryResolve(out IMvxAppStart startup) && !startup.IsStarted)
            {
                startup.Start(GetAppStartHint(hint));
            }
        }

        protected virtual object GetAppStartHint(object hint = null)
        {
            return hint;
        }

        protected virtual void RegisterSetup()
        {
        }
    }

    public class MesApplication<TMvxWpfSetup, TApplication> : MesApplication
       where TMvxWpfSetup : MesAvnSetup<TApplication>, new()
       where TApplication : class, IMvxApplication, new()
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<TMvxWpfSetup>();
        }
    }
}
