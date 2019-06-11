using System.Drawing;
using System.Windows;

namespace MinoriEditorStudio.VirtualCanvas.Services
{
    public interface IQuadNode<T>
    {
        RectangleF Bounds { get; }
        IQuadNode<T> Next { get; set; }
        T Node { get; set; }
    }
}