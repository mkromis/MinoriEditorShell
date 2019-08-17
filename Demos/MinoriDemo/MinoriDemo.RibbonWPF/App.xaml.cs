using MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Models;
using MinoriEditorStudio.Platforms.Wpf;
using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorStudio.VirtualCanvas.Service;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace MinoriDemo.RibbonWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        public override void ApplicationInitialized()
        {
            base.ApplicationInitialized();
            Mvx.IoCProvider.RegisterType<IVirtualChild, TestShape>();
        }

        protected override void RegisterSetup() => this.RegisterSetupType<MesWpfSetup<Core.App>>();
    }
}
