//-----------------------------------------------------------------------
// <copyright file="QuadTree.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorStudio.VirtualCanvas.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Models
{

    /// <summary>
    /// This class efficiently stores and retrieves arbitrarily sized and positioned
    /// objects in a quad-tree data structure.  This can be used to do efficient hit
    /// detection or visiblility checks on objects in a virtualized canvas.
    /// The object does not need to implement any special interface because the Rect Bounds
    /// of those objects is handled as a separate argument to Insert.
    /// </summary>
    public partial class QuadTree<T> : IQuadTree<T> where T : class
    {
        RectangleF _bounds; // overall bounds we are indexing.
        public IQuadrant<T> Root { get; private set; }
        IDictionary<T, IQuadrant<T>> _table;

        /// <summary>
        /// This determines the overall quad-tree indexing strategy, changing this bounds
        /// is expensive since it has to re-divide the entire thing - like a re-hash operation.
        /// </summary>
        public RectangleF Bounds
        {
            get => _bounds;
            set { _bounds = value; ReIndex(); }
        }

        /// <summary>
        /// Insert a node with given bounds into this QuadTree.
        /// </summary>
        /// <param name="node">The node to insert</param>
        /// <param name="bounds">The bounds of this node</param>
        public void Insert(T node, RectangleF bounds)
        {
            if (_bounds.Width == 0 || _bounds.Height == 0)
            {
                // todo: localize.
                throw new InvalidOperationException("You must set a non-zero bounds on the QuadTree first");
            }
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                // todo: localize.
                throw new InvalidOperationException("Inserted node must have a non-zero width and height");
            }
            if (Root == null)
            {
                Root = new Quadrant<T>(null, _bounds);
            }

            IQuadrant<T> parent = Root.Insert(node, bounds);

            if (_table == null)
            {
                _table = new Dictionary<T, IQuadrant<T>>();
            }
            _table[node] = parent;


        }

        /// <summary>
        /// Get a list of the nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>List of zero or mode nodes found inside the given bounds</returns>
        public IEnumerable<T> GetNodesInside(RectangleF bounds)
        {
            foreach (QuadNode<T> n in GetNodes(bounds))
            {
                yield return n.Node;
            }
        }

        /// <summary>
        /// Get a list of the nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>List of zero or mode nodes found inside the given bounds</returns>
        public Boolean HasNodesInside(RectangleF bounds)
        {
            if (Root != null)
            {
                Root.HasIntersectingNodes(bounds);
            }
            return false;
        }

        /// <summary>
        /// Get list of nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>The list of nodes intersecting the given bounds</returns>
        public IEnumerable<IQuadNode<T>> GetNodes(RectangleF bounds)
        {
            List<IQuadNode<T>> result = new List<IQuadNode<T>>();
            if (Root != null)
            {
                Root.GetIntersectingNodes(result, bounds);
            }
            return result;
        }

        /// <summary>
        /// Remove the given node from this QuadTree.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>True if the node was found and removed.</returns>
        public Boolean Remove(T node)
        {
            if (_table != null)
            {
                if (_table.TryGetValue(node, out IQuadrant<T> parent))
                {
                    parent.RemoveNode(node);
                    _table.Remove(node);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Rebuild all the Quadrants according to the current QuadTree Bounds.
        /// </summary>
        void ReIndex()
        {
            Root = null;
            foreach (IQuadNode<T> n in GetNodes(_bounds))
            {
                // todo: it would be more efficient if we added a code path that allowed
                // reuse of the QuadNode wrappers.
                Insert(n.Node, n.Bounds);
            }
        }

        /// <summary>
        /// Staticail visual information
        /// </summary>
        /// <param name="container"></param>
        public void ShowQuadTree(Object container) => Root?.ShowQuadTree(container);
    }
}
