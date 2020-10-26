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

namespace MinoriEditorShell.Platforms.Avalonia.ViewModels
{
    ///<summary>This is a bridge between MvvmCross and Avalonia project
    public abstract class MesApplication : global::Avalonia.Application, IMvxApplication
    {
        private IMvxViewModelLocator _defaultLocator;

        private IMvxViewModelLocator DefaultLocator
        {
            get
            {
                _defaultLocator = _defaultLocator ?? CreateDefaultViewModelLocator();
                return _defaultLocator;
            }
        }

        protected virtual IMvxViewModelLocator CreateDefaultViewModelLocator()
        {
            return new MvxDefaultViewModelLocator();
        }

        public virtual void LoadPlugins(IMvxPluginManager pluginManager)
        {
            // do nothing
        }

        /// <summary>
        /// Any initialization steps that can be done in the background
        /// </summary>
        // public virtual override void Initialize()
        // {
        //     base.Initialize();
        //     // do nothing
        // }

        /// <summary>
        /// Any initialization steps that need to be done on the UI thread
        /// </summary>
        public virtual Task Startup()
        {
            IMvxLog log = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor<MesApplication>();
            log.Trace("AppStart: Application Startup - On UI thread");
            return Task.CompletedTask;
        }

        /// <summary>
        /// If the application is restarted (eg primary activity on Android 
        /// can be restarted) this method will be called before Startup
        /// is called again
        /// </summary>
        public virtual void Reset()
        {
            // do nothing
        }

        public IMvxViewModelLocator FindViewModelLocator(MvxViewModelRequest request)
        {
            return DefaultLocator;
        }

        protected void RegisterCustomAppStart<TMvxAppStart>()
            where TMvxAppStart : class, IMvxAppStart
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, TMvxAppStart>();
        }

        protected void RegisterAppStart<TViewModel>()
            where TViewModel : IMvxViewModel
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<TViewModel>>();
        }

        protected void RegisterAppStart(IMvxAppStart appStart)
        {
            Mvx.IoCProvider.RegisterSingleton(appStart);
        }

        protected virtual void RegisterAppStart<TViewModel, TParameter>()
          where TViewModel : IMvxViewModel<TParameter> where TParameter : class
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<TViewModel, TParameter>>();
        }

        protected IEnumerable<Type> CreatableTypes()
        {
            return CreatableTypes(GetType().GetTypeInfo().Assembly);
        }

        protected IEnumerable<Type> CreatableTypes(Assembly assembly)
        {
            return assembly.CreatableTypes();
        }
    }

    public class MvxApplication<TParameter> : MvxApplication, IMvxApplication<TParameter>
    {
        public virtual TParameter Startup(TParameter parameter)
        {
            // do nothing, so just return the original hint
            return parameter;
        }
    }
}