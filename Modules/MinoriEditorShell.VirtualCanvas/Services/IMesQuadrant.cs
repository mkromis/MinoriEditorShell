using System.Collections.Generic;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    /// <summary>
    /// The canvas is split up into four Quadrants and objects are stored in the quadrant that contains them
    /// and each quadrant is split up into four child Quadrants recursively.  Objects that overlap more than
    /// one quadrant are stored in the _nodes list for this Quadrant.
    /// </summary>
    public interface IMesQuadrant<T>
    {
        /// <summary>
        /// The quadrant is subdivided when nodes are inserted that are
        /// completely contained within those subdivisions.
        /// </summary>
        IMesQuadrant<T> BottomLeft { get; }

        /// <summary>
        /// The quadrant is subdivided when nodes are inserted that are
        /// completely contained within those subdivisions.
        /// </summary>
        IMesQuadrant<T> BottomRight { get; }

        /// <summary>
        /// The bounds of this quadrant
        /// </summary>
        RectangleF Bounds { get; }

        /// <summary>
        /// nodes that overlap the sub quadrant boundaries.
        /// </summary>
        IMesQuadNode<T> Nodes { get; }

        /// <summary>
        /// The quadrant is subdivided when nodes are inserted that are
        /// completely contained within those subdivisions.
        /// </summary>
        IMesQuadrant<T> TopLeft { get; }

        /// <summary>
        /// The quadrant is subdivided when nodes are inserted that are
        /// completely contained within those subdivisions.
        /// </summary>
        IMesQuadrant<T> TopRight { get; }

        /// <summary>
        /// Statictial information for rendering use.
        /// </summary>
        /// <param name="o"></param>
        void ShowQuadTree(object o);

        /// <summary>
        /// Return true if there are any nodes in this Quadrant that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>boolean</returns>
        bool HasIntersectingNodes(RectangleF bounds);

        /// <summary>
        /// Returns all nodes in this quadrant that intersect the given bounds.
        /// The nodes are returned in pretty much random order as far as the caller is concerned.
        /// </summary>
        /// <param name="nodes">List of nodes found in the given bounds</param>
        /// <param name="bounds">The bounds that contains the nodes you want returned</param>
        void GetIntersectingNodes(IList<IMesQuadNode<T>> nodes, RectangleF bounds);

        /// <summary>
        /// Walk the given linked list of QuadNodes and check them against the given bounds.
        /// Add all nodes that intersect the bounds in to the list.
        /// </summary>
        /// <param name="last">The last QuadNode in a circularly linked list</param>
        /// <param name="nodes">The resulting nodes are added to this list</param>
        /// <param name="bounds">The bounds to test against each node</param>
        void GetIntersectingNodes(IMesQuadNode<T> last, IList<IMesQuadNode<T>> nodes, RectangleF bounds);

        /// <summary>
        /// Insert the given node
        /// </summary>
        /// <param name="node">The node </param>
        /// <param name="bounds">The bounds of that node</param>
        /// <returns></returns>
        IMesQuadrant<T> Insert(T node, RectangleF bounds);
        /// <summary>
        /// Remove the given node from this Quadrant.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>Returns true if the node was found and removed.</returns>
        bool RemoveNode(T node);

        /// <summary>
        /// Walk the given linked list and test each node against the given bounds/
        /// </summary>
        /// <param name="last">The last node in the circularly linked list.</param>
        /// <param name="bounds">Bounds to test</param>
        /// <returns>Return true if a node in the list intersects the bounds</returns>
        bool HasIntersectingNodes(IMesQuadNode<T> last, RectangleF bounds);
    }
}