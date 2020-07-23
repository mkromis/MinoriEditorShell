using MinoriDemo.Core.ViewModels;
using MvvmCross.ViewModels;

namespace MinoriDemo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize() => RegisterAppStart<MainViewModel>();
    }
}