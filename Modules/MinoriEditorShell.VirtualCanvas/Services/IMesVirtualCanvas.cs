using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.VirtualCanvas.Services
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
    ///    HorizontalScrollBarVisibility="Auto" 
    ///    VerticalScrollBarVisibility="Auto" 
    ///    CanContentScroll="True"&gt;
    ///    &lt;MesVirtualCanvas x:Name="Graph" /&gt;
    /// &lt;/ScrollViewer&gt;
    /// </code>
    /// 
    /// You would then need to call implement a view ctor simular to below
    /// 
    /// DataContextChanged += (s, e) =>
    /// {
    ///     IMesVirtualCanvas dc = (IMesVirtualCanvas)DataContext;
    ///     Graph.UseDefaultControls(dc);
    /// };
    /// </summary>
    public interface IMesVirtualCanvas
    {
        IMesAutoScroll AutoScroll { get; set; }
        IMesVirtualCanvasControl Graph { get; set; }
        IMesPan Pan { get; set;  }
        IMesRectangleSelectionGesture RectZoom { get; set; }
        IMesMapZoom Zoom { get; set; }
    }
}