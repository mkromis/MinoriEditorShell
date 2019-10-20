using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Models;
using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross;
using MvvmCross.Plugin;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterType<IMesAutoScroll, MesAutoScroll>();
            Mvx.IoCProvider.RegisterType<IMesContentCanvas, MesContentCanvas>();
            Mvx.IoCProvider.RegisterType<IMesMapZoom, MesMapZoom>();
            Mvx.IoCProvider.RegisterType<IMesPan, MesPan>();
            Mvx.IoCProvider.RegisterType<IMesRectangleSelectionGesture, MesRectangleSelectionGesture>();
            Mvx.IoCProvider.RegisterType<IMesVirtualCanvasControl, MesVirtualCanvas>();

        }
    }
}
