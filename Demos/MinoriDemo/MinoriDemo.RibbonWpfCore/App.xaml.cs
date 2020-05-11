using MinoriDemo.Core.Modules.VirtualCanvas.Models;
using MinoriDemo.RibbonWpfCore.Modules.VirtualCanvas.Models;
using MinoriEditorShell.Platforms.Wpf;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace MinoriDemo.RibbonWpfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        public override void ApplicationInitialized()
        {
            base.ApplicationInitialized();
            Mvx.IoCProvider.RegisterType<ITestShape, TestShape>();
        }

        protected override void RegisterSetup() => this.RegisterSetupType<Setup>();
    }
}
