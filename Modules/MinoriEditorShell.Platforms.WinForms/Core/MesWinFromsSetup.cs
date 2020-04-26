﻿using MvvmCross.Plugin;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Console.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace MinoriEditorShell.Platforms.WinForms.Core
{
    public abstract class MesWinFormsSetup : MvxSetup
    {
        //private Dispatcher _uiThreadDispatcher;
        //private ContentControl _root;
        //private IMvxWpfViewPresenter _presenter;

        //public void PlatformInitialize(Dispatcher uiThreadDispatcher, IMvxWpfViewPresenter presenter)
        //{
        //    _uiThreadDispatcher = uiThreadDispatcher;
        //    _presenter = presenter;
        //}

        //public void PlatformInitialize(Dispatcher uiThreadDispatcher, ContentControl root)
        //{
        //    _uiThreadDispatcher = uiThreadDispatcher;
        //    _root = root;
        //}

        public override IEnumerable<Assembly> GetViewAssemblies()
        {
            return base.GetViewAssemblies().Union(new[] { Assembly.GetEntryAssembly() });
        }

        protected sealed override IMvxViewsContainer CreateViewsContainer()
        {
            //var toReturn = CreateWpfViewsContainer();
            //Mvx.IoCProvider.RegisterSingleton<IMvxWpfViewLoader>(toReturn);
            //return toReturn;
            return null;
        }

        //protected virtual IMvxWpfViewsContainer CreateWpfViewsContainer()
        //{
        //    return new MvxWpfViewsContainer();
        //}

        //protected IMvxWpfViewPresenter Presenter
        //{
        //    get
        //    {
        //        _presenter = _presenter ?? CreateViewPresenter(_root);
        //        return _presenter;
        //    }
        //}

        //protected virtual IMvxWpfViewPresenter CreateViewPresenter(ContentControl root)
        //{
        //    return new MvxWpfViewPresenter(root);
        //}

        protected override IMvxViewDispatcher CreateViewDispatcher()
        {
            //    //return new MvxWpfViewDispatcher(_uiThreadDispatcher, Presenter);
            return null;
        }

        protected virtual void RegisterPresenter()
        {
            //var presenter = Presenter;
            //Mvx.IoCProvider.RegisterSingleton(presenter);
            //Mvx.IoCProvider.RegisterSingleton<IMvxViewPresenter>(presenter);
        }

        protected override IMvxNameMapping CreateViewToViewModelNaming()
        {
            return new MvxPostfixAwareViewToViewModelNameMapping("View", "Control");
        }

        protected override void InitializeFirstChance()
        {
            //RegisterPresenter();
            base.InitializeFirstChance();
        }

        protected override void InitializeLastChance()
        {
            InitializeBindingBuilder();
            base.InitializeLastChance();
        }

        protected virtual void InitializeBindingBuilder()
        {
            //RegisterBindingBuilderCallbacks();
            //var bindingBuilder = CreateBindingBuilder();
            //bindingBuilder.DoRegistration();
        }

        //protected virtual void RegisterBindingBuilderCallbacks()
        //{
        //    Mvx.IoCProvider.CallbackWhenRegistered<IMvxValueConverterRegistry>(FillValueConverters);
        //    Mvx.IoCProvider.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(FillTargetFactories);
        //    Mvx.IoCProvider.CallbackWhenRegistered<IMvxBindingNameRegistry>(FillBindingNames);
        //}

        //protected virtual void FillBindingNames(IMvxBindingNameRegistry registry)
        //{
        //    // this base class does nothing
        //}

        //protected virtual void FillValueConverters(IMvxValueConverterRegistry registry)
        //{
        //    registry.Fill(ValueConverterAssemblies);
        //    registry.Fill(ValueConverterHolders);
        //}

        //protected virtual void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        //{
        //    // this base class does nothing
        //}

        protected virtual List<Type> ValueConverterHolders => new List<Type>();

        protected virtual IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = new List<Assembly>();
                toReturn.AddRange(GetViewModelAssemblies());
                toReturn.AddRange(GetViewAssemblies());
                return toReturn;
            }
        }

        //protected virtual MvxBindingBuilder CreateBindingBuilder()
        //{
        //    return new MvxWindowsBindingBuilder();
        //}
    }

    public class MesWinFormsSetup<TApplication> : MesWinFormsSetup
        where TApplication : class, IMvxApplication, new()
    {
        protected override IMvxApplication CreateApp() => Mvx.IoCProvider.IoCConstruct<TApplication>();
    }
}