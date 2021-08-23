using Avalonia.Controls;
using Avalonia.Threading;
using MinoriEditorShell.Platforms.Avalonia.Presenters;
using MvvmCross.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Core
{
    /// <summary>
    /// Setup utilities
    /// </summary>
    public class MvxAvnSetupSingleton : MvxSetupSingleton
    {
        /// <summary>
        /// Helper to ensure seup is only done once
        /// </summary>
        /// <param name="uiThreadDispatcher"></param>
        /// <param name="presenter"></param>
        /// <returns></returns>
        public static MvxAvnSetupSingleton EnsureSingletonAvailable(Dispatcher uiThreadDispatcher, IMesAvnViewPresenter presenter)
        {
            MvxAvnSetupSingleton instance = EnsureSingletonAvailable<MvxAvnSetupSingleton>();
            instance.PlatformSetup<MesAvnSetup>()?.PlatformInitialize(uiThreadDispatcher, presenter);
            return instance;
        }

        /// <summary>
        /// Helper to make sure setup is only done once.
        /// </summary>
        /// <param name="uiThreadDispatcher"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static MvxAvnSetupSingleton EnsureSingletonAvailable(Dispatcher uiThreadDispatcher, ContentControl root)
        {
            MvxAvnSetupSingleton instance = EnsureSingletonAvailable<MvxAvnSetupSingleton>();
            instance.PlatformSetup<MesAvnSetup>()?.PlatformInitialize(uiThreadDispatcher, root);
            return instance;
        }
    }
}
