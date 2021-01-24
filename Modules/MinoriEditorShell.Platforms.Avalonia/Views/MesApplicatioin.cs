using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Logging;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    ///<summary>This is a bridge between MvvmCross and Avalonia project
    public abstract class MesApplication : Application
    {
        public MesApplication() : base()
        {
            RegisterSetup();
        }

        public virtual void ApplicationInitialized()
        {
            if (MainWindow == null) return;

            // MvxWpfSetupSingleton.EnsureSingletonAvailable(Dispatcher, MainWindow).EnsureInitialized();

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

    // public class MvxApplication<TMvxWpfSetup, TApplication> : MvxApplication
    //    where TMvxWpfSetup : MvxWpfSetup<TApplication>, new()
    //    where TApplication : class, IMvxApplication, new()
    // {
    //     protected override void RegisterSetup()
    //     {
    //         this.RegisterSetupType<TMvxWpfSetup>();
    //     }
    // }
}