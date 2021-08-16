using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    /// <summary>
    /// Main application helper to help setup IoC
    /// </summary>
    public abstract class MesApplication : Application
    {

        /// <summary>
        /// Main application class interface
        /// </summary>
        protected MesApplication() : base() => RegisterSetup();

        public virtual void ApplicationInitialized()
        {
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

        protected abstract void RegisterSetup();
    }

    /// <summary>
    /// This is an Generics helper for MesApplication for 
    /// </summary>
    /// <typeparam name="TMvxWpfSetup"></typeparam>
    /// <typeparam name="TApplication"></typeparam>
    public class MesApplication<TMvxWpfSetup, TApplication> : MesApplication
       where TMvxWpfSetup : MesAvnSetup<TApplication>, new()
       where TApplication : class, IMvxApplication, new()
    {

        /// <summary>
        /// Register setup based on template
        /// </summary>
        protected override void RegisterSetup() => this.RegisterSetupType<TMvxWpfSetup>();
    }
}
