using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        /// <summary>
        /// Helper to load types for usage with views.
        /// </summary>
        /// <param name="provider"></param>
        public void Load(IMvxIoCProvider provider)
        {
            provider.RegisterType<IMesAutoScroll, MesAutoScroll>();
            provider.RegisterType<IMesContentCanvas, MesContentCanvas>();
            provider.RegisterType<IMesMapZoom, MesMapZoom>();
            provider.RegisterType<IMesPan, MesPan>();
            provider.RegisterType<IMesRectangleSelectionGesture, MesRectangleSelectionGesture>();
            provider.RegisterType<IMesVirtualCanvasControl, MesVirtualCanvas>();
        }
    }
}