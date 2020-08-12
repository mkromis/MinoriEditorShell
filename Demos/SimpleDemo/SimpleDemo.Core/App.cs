using MvvmCross;
using MvvmCross.ViewModels;
using SimpleDemo.Core.Services;
using SimpleDemo.Core.ViewModels;

namespace SimpleDemo.Core
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