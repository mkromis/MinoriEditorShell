using MinoriEditorShell.VirtualCanvas.Services;
using System;

namespace MinoriEditorShell.VirtualCanvas.Extensions
{
    /// <summary>
    /// Sets the zoom property, or manual if user changed
    /// </summary>
    public enum ZoomToContent
    {
        /// <summary>
        /// User definded size
        /// </summary>
        Manual = 0,

        /// <summary>
        /// request zoom by width
        /// </summary>
        Width,

        /// <summary>
        /// Request zoom by height
        /// </summary>
        Height,

        /// <summary>
        /// Reuqest to zoom by both
        /// </summary>
        WidthAndHeight
    }

    /// <summary>
    /// Extensions for IMesVirtualCanvas used in place of inheritance.
    /// </summary>
    public static class IMesVirtualCanvasExtensions
    {
        /// <summary>
        /// Zoom content to view. Must be done on IVirtualCanvas due to needing zoom and graph
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="zoomToContent"></param>
        public static void ZoomToContent(this IMesVirtualCanvas canvas, ZoomToContent zoomToContent)
        {
            double? zoom = null;
            switch (zoomToContent)
            {
                case Extensions.ZoomToContent.Manual:
                    zoom = null;
                    break;

                case Extensions.ZoomToContent.Width:
                    zoom = canvas.Graph.ViewportWidth / canvas.Graph.Extent.Width;
                    break;

                case Extensions.ZoomToContent.Height:
                    zoom = canvas.Graph.ViewportHeight / canvas.Graph.Extent.Width;
                    break;

                case Extensions.ZoomToContent.WidthAndHeight:
                    double scaleX = canvas.Graph.ViewportWidth / canvas.Graph.Extent.Width;
                    double scaleY = canvas.Graph.ViewportHeight / canvas.Graph.Extent.Height;

                    zoom = Math.Min(scaleX, scaleY);
                    break;
            }

            if (zoom != null)
            {
                canvas.Zoom.Value = zoom.Value;
                canvas.Zoom.ResetTranslate();
            }
        }
    }
}