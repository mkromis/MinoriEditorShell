//-----------------------------------------------------------------------
// <copyright file="QuadTree.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorShell.VirtualCanvas.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MinoriEditorShell.VirtualCanvas.Models
{
    /// <inheritdoc cref="IMesQuadTree{T}"/>
    public class MesQuadTree<T> : IMesQuadTree<T> where T : class
    {
        private RectangleF _bounds; // overall bounds we are indexing.
        /// <inheritdoc />
        public IMesQuadrant<T> Root { get; private set; }
        private IDictionary<T, IMesQuadrant<T>> _table;
        /// <inheritdoc />
        public RectangleF Bounds
        {
            get => _bounds;
            set { _bounds = value; ReIndex(); }
        }

        /// <inheritdoc />
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
                Root = new MesQuadrant<T>(null, _bounds);
            }

            IMesQuadrant<T> parent = Root.Insert(node, bounds);

            if (_table == null)
            {
                _table = new Dictionary<T, IMesQuadrant<T>>();
            }
            _table[node] = parent;
        }

        /// <inheritdoc />
        public IEnumerable<T> GetNodesInside(RectangleF bounds) => 
            GetNodes(bounds).OfType<MesQuadNode<T>>().Select(n => n.Node);
        /// <inheritdoc />
        public bool HasNodesInside(RectangleF bounds)
        {
            Root?.HasIntersectingNodes(bounds);
            return false;
        }

        /// <inheritdoc />
        public IEnumerable<IMesQuadNode<T>> GetNodes(RectangleF bounds)
        {
            List<IMesQuadNode<T>> result = new List<IMesQuadNode<T>>();
            Root?.GetIntersectingNodes(result, bounds);
            return result;
        }

        /// <inheritdoc />
        public bool Remove(T node)
        {
            if (_table == null || !_table.TryGetValue(node, out IMesQuadrant<T> parent))
            {
                return false;
            }

            parent.RemoveNode(node);
            _table.Remove(node);
            return true;
        }

        /// <summary>
        /// Rebuild all the Quadrants according to the current QuadTree Bounds.
        /// </summary>
        private void ReIndex()
        {
            Root = null;
            foreach (IMesQuadNode<T> n in GetNodes(_bounds))
            {
                // todo: it would be more efficient if we added a code path that allowed
                // reuse of the QuadNode wrappers.
                Insert(n.Node, n.Bounds);
            }
        }

        /// <inheritdoc />
        public void ShowQuadTree(object container) => 
            Root?.ShowQuadTree(container);
    }
}