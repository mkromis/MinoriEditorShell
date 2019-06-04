using MinoriEditorStudio.VirtualCanvas.Service;
using MinoriEditorStudio.VirtualCanvas.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorStudio.VirtualCanvas
{
    [MvxPlugin]
    class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterType<Views.VirtualCanvasView>();
            Mvx.IoCProvider.RegisterType<IVirtualCanvas>(() =>
            {
                Mvx.IoCProvider.Resolve<IMvxViewsContainer>().Add<VirtualCanvasViewModel, Views.VirtualCanvasView>();
                return new VirtualCanvasViewModel();
            });

            // Add specific views
        }
    }
}
