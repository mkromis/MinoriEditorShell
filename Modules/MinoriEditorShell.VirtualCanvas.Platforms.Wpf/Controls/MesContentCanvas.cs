using MinoriEditorShell.VirtualCanvas.Services;
using System.Drawing;
using System.Windows.Controls;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls
{
    /// <summary>
    /// A wpf interface used to instanciate into a netstandard object
    /// </summary>
    public class MesContentCanvas : Canvas, IMesContentCanvas
    {
        /// <summary>
        /// Sets the background color of the canvas object
        /// </summary>
        /// <param name="color"></param>
        public void SetCanvasBackgroundColor(Color color)
        {
            System.Windows.Media.Color newcolor =
                System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
            System.Windows.Media.Brush brush = new System.Windows.Media.SolidColorBrush(newcolor);
            Background = brush;
        }
    }
}