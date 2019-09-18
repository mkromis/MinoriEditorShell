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
    /// You would then need to call implement a view ctor simular to below
    /// 
    /// DataContextChanged += (s, e) =>
    /// {
    ///     VirtualCanvasViewModel dc = (VirtualCanvasViewModel)DataContext;
    ///     dc.Graph = Graph;
    ///     
    ///     IContentCanvas canvas = dc.Graph.ContentCanvas;
    ///     dc.Zoom = new MapZoom(canvas);
    ///     dc.Pan = new Pan(canvas, dc.Zoom);
    ///     dc.AutoScroll = new AutoScroll(canvas, dc.Zoom);
    ///     dc.RectZoom = new RectangleSelectionGesture(canvas, dc.Zoom);
    /// };
    /// </summary>
    public class VirtualCanvasViewModel : Document, IVirtualCanvas
    {
        /// <summary>
        /// Platform zoom interface
        /// </summary>
        public IMapZoom Zoom { get; set; }
        /// <summary>
        /// Platform Pan interface
        /// </summary>
        public IPan Pan { get; set; }
        /// <summary>
        /// Rectangle Zoom platform interface
        /// </summary>
        public IRectangleSelectionGesture RectZoom { get; set; }
        /// <summary>
        /// Autoscroll zoom platform interface
        /// </summary>
        public IAutoScroll AutoScroll { get; set; }
        /// <summary>
        /// Canvas Control platform interface
        /// </summary>
        public IVirtualCanvasControl Graph { get; set; }
    }
}
