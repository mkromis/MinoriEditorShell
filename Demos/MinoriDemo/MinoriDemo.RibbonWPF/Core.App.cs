using MinoriDemo.RibbonWPF.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace MinoriDemo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<MinoriDemo.RibbonWPF.Modules.VirtualCanvas.ViewModels.VirtualCanvasViewModel>();

            RegisterAppStart<MainViewModel>();
        }
    }
}
