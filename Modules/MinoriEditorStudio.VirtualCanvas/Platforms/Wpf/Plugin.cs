using MinoriEditorStudio.VirtualCanvas.ViewModels;
using MinoriEditorStudio.VirtualCanvas.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorStudio.VirtualCanvas.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            //Mvx.IoCProvider.RegisterType<IVirtualCanvas, VirtualCanvasView>();
            //Mvx.IoCProvider.RegisterType<IVirtualCanvas>(() =>
            //{
            //    Mvx.IoCProvider.Resolve<IMvxViewsContainer>().Add<VirtualCanvasViewModel, VirtualCanvasView>();
            //    return new VirtualCanvasViewModel();
            //});

            // Add specific views
        }
    }
}
