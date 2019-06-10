namespace MinoriEditorStudio.VirtualCanvas.Service
{
    public interface IVirtualCanvasControl
    {
        IContentCanvas ContentCanvas { get; }
        IMapZoom Zoom { get; set; }
    }
}