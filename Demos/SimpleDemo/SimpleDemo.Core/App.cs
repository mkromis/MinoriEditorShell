using MvvmCross;
using MvvmCross.ViewModels;
using SimpleDemo.Core.Services;
using SimpleDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDemo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<ICalculationService, CalculationService>();

            RegisterAppStart<TipViewModel>();
        }
    }
}
