using MinoriEditorStudio.VirtualCanvas.Services;
using System;
using System.Threading.Tasks;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.VirtualCanvas.ViewModels
{
    /// <summary>
    /// This demo shows the VirtualCanvas managing up to 50,000 random WPF shapes providing smooth scrolling and
    /// zooming while creating those shapes on the fly.  This helps make a WPF canvas that is a lot more
    /// scalable.
    /// 
    /// Wrap your virtual canvas into a Scroll Viewer
    /// for example
    /// <code>
    /// xmlns:ui="clr-namespace:MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls;assembly=MinoriEditorStudio.VirtualCanvas"
    /// 
    /// &lt;ScrollViewer
    ///    x:Name="Scroller" 
    ///    HorizontalScrollBarVisibility="Auto" 
    ///    VerticalScrollBarVisibility="Auto" 
    ///    CanContentScroll="True"&gt;
    ///    &lt;VirtualCanvas x:Name="Graph" /&gt;
    /// &lt;/ScrollViewer&gt;
    /// </code>
    /// 
    /// You would then need to call implement a view appeared simular to below
    ///public override void ViewAppeared()
    ///{
    ///    // This is being called twice or not at all.
    ///    if (Graph == null)
    ///    {
    ///        Graph = ((VirtualCanvasView)View).Graph;
    ///        IContentCanvas target = Graph.ContentCanvas;
    ///        Graph.Zoom = Zoom = new MapZoom(target);
    ///        Pan = new Pan(target, Zoom);
    ///        AutoScroll = new AutoScroll(target, Zoom);
    ///        RectZoom = new RectangleSelectionGesture(target, Zoom);
    ///    }
    ///}
    /// </summary>
    public class VirtualCanvasViewModel : Document, IVirtualCanvas
    {
        public IMapZoom Zoom { get; protected set; }
        public IPan Pan { get; protected set; }
        public IRectangleSelectionGesture RectZoom { get; protected set; }
        public IAutoScroll AutoScroll { get; protected set; }
        public IVirtualCanvasControl Graph { get; protected set; }
        public new Boolean CanClose { get; set; }
    }
}
