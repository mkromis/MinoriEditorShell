using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    /// <summary>
    /// This class efficiently stores and retrieves arbitrarily sized and positioned
    /// objects in a quad-tree data structure.  This can be used to do efficient hit
    /// detection or visibility checks on objects in a virtualized canvas.
    /// The object does not need to implement any special interface because the Rect Bounds
    /// of those objects is handled as a separate argument to Insert.
    /// </summary>
    public interface IMesQuadTree<T> where T : class
    {
        /// <summary>
        /// This determines the overall quad-tree indexing strategy, changing this bounds
        /// is expensive since it has to re-divide the entire thing - like a re-hash operation.
        /// </summary>
        RectangleF Bounds { get; set; }
        /// <summary>
        /// Root object for displaying of objects
        /// </summary>
        IMesQuadrant<T> Root { get; }
        /// <summary>
        /// Get list of nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>The list of nodes intersecting the given bounds</returns>
        IEnumerable<IMesQuadNode<T>> GetNodes(RectangleF bounds);

        /// <summary>
        /// Get a list of the nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>List of zero or mode nodes found inside the given bounds</returns>
        IEnumerable<T> GetNodesInside(RectangleF bounds);
        /// <summary>
        /// Get a list of the nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>List of zero or mode nodes found inside the given bounds</returns>
        bool HasNodesInside(RectangleF bounds);
        /// <summary>
        /// Insert a node with given bounds into this QuadTree.
        /// </summary>
        /// <param name="node">The node to insert</param>
        /// <param name="bounds">The bounds of this node</param>
        void Insert(T node, RectangleF bounds);
        /// <summary>
        /// Remove the given node from this QuadTree.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>True if the node was found and removed.</returns>
        bool Remove(T node);
        /// <summary>
        /// Statical visual information
        /// </summary>
        /// <param name="container"></param>
        void ShowQuadTree(object container);
    }
}