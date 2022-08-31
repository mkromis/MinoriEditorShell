using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf
{
    // Using long name to remove warning about Plugin name collision
    /// <summary>
    /// Plugin to assist in setup of virtual canvas for wpf
    /// </summary>
    [MvvmCross.Plugin.MvxPlugin]
    public class Plugin : MvvmCross.Plugin.IMvxPlugin
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