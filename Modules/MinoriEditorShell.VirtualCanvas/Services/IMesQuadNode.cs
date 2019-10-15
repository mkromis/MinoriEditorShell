using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesQuadNode<T>
    {
        RectangleF Bounds { get; }
        IMesQuadNode<T> Next { get; set; }
        T Node { get; set; }
    }
}