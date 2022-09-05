using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    /// <summary>
    /// Each node stored in the tree has a position, width and height.
    /// </summary>
    public interface IMesQuadNode<T>
    {
        /// <summary>
        /// The Rect bounds of the node
        /// </summary>
        RectangleF Bounds { get; }
        /// <summary>
        /// QuadNodes form a linked list in the Quadrant.
        /// </summary>
        IMesQuadNode<T> Next { get; set; }
        /// <summary>
        /// The node
        /// </summary>
        T Node { get; set; }
    }
}