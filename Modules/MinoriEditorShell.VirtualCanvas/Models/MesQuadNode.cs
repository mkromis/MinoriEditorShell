//-----------------------------------------------------------------------
// <copyright file="QuadTree.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorShell.VirtualCanvas.Services;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Models
{
    /// <inheritdoc cref="IMesQuadNode{T}"/>
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
        /// <inheritdoc />
        public T Node { get; set; }
        /// <inheritdoc />
        public RectangleF Bounds { get; }
        /// <inheritdoc />
        public IMesQuadNode<T> Next { get; set; }
    }
}