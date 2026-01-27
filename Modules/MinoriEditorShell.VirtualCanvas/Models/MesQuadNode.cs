//-----------------------------------------------------------------------
// <copyright file="QuadTree.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorShell.VirtualCanvas.Services;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Models
{
    /// <summary>
    /// Each node stored in the tree has a position, width and height.
    /// </summary>
    internal class MesQuadNode<T> : IMesQuadNode<T>
    {
        /// <summary>
        /// Construct new QuadNode to wrap the given node with given bounds
        /// </summary>
        /// <param name="node">The node</param>
        /// <param name="bounds">The bounds of that node</param>
        public MesQuadNode(T node, RectangleF bounds)
        {
            Node = node;
            Bounds = bounds;
        }

        /// <summary>
        /// The node
        /// </summary>
        public T Node { get; set; }

        /// <summary>
        /// The Rect bounds of the node
        /// </summary>
        public RectangleF Bounds { get; }

        /// <summary>
        /// QuadNodes form a linked list in the Quadrant.
        /// </summary>
        public IMesQuadNode<T> Next { get; set; }
    }
}