using MinoriEditorStudio.Modules.Themes.Services;
using MvvmCross;
using MvvmCross.ViewModels;
using MinoriDemo.Core.Services;
using MinoriDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriDemo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<ICalculationService, CalculationService>();

            RegisterAppStart<MainViewModel>();
        }
    }
}
