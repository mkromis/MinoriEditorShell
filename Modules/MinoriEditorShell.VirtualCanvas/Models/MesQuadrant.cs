//-----------------------------------------------------------------------
// <copyright file="QuadTree.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorShell.VirtualCanvas.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Models
{
    /// <inheritdoc cref="IMesQuadNode{T}"/>
    internal class MesQuadrant<T> : IMesQuadrant<T>
    {
        private readonly IMesQuadrant<T> _parent;

        /// <inheritdoc/>
        public RectangleF Bounds { get; private set; } 
        /// <inheritdoc/>
        public IMesQuadNode<T> Nodes { get; private set; }
        /// <inheritdoc/>
        public IMesQuadrant<T> TopLeft { get; private set; }
        /// <inheritdoc/>
        public IMesQuadrant<T> TopRight { get; private set; }
        /// <inheritdoc/>
        public IMesQuadrant<T> BottomLeft { get; private set; }
        /// <inheritdoc/>
        public IMesQuadrant<T> BottomRight { get; private set; }

        /// <inheritdoc/>
        public void ShowQuadTree(object o)
        {
            //if (o is Canvas c)
            //{
            //    //Rectangle r = new Rectangle
            //    //{
            //    //    Width = Bounds.Width,
            //    //    Height = Bounds.Height
            //    //};

            //    //Canvas.SetLeft(r, Bounds.);
            //    //Canvas.SetTop(r, Bounds.Top);
            //    //r.Stroke = Brushes.DarkRed;
            //    //r.StrokeThickness = 1;
            //    //r.StrokeDashArray = new DoubleCollection(new Double[] { 2.0, 3.0 });
            //    //c.Children.Add(r);

            //    //TopLeft?.ShowQuadTree(c);
            //    //TopRight?.ShowQuadTree(c);
            //    //BottomLeft?.ShowQuadTree(c);
            //    //BottomRight?.ShowQuadTree(c);
            //}
            throw new NotImplementedException();
        }

        /// <summary>
        /// Construct new Quadrant with a given bounds all nodes stored inside this quadrant
        /// will fit inside this bounds.
        /// </summary>
        /// <param name="parent">The parent quadrant (if any)</param>
        /// <param name="bounds">The bounds of this quadrant</param>
        public MesQuadrant(IMesQuadrant<T> parent, RectangleF bounds)
        {
            _parent = parent;
            Debug.Assert(bounds.Width != 0 && bounds.Height != 0);
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                // todo: localize
                throw new ArgumentException("Bounds of quadrant cannot be zero width or height");
            }
            Bounds = bounds;
        }
        /// <inheritdoc />
        public IMesQuadrant<T> Insert(T node, RectangleF bounds)
        {
            Debug.Assert(bounds.Width != 0 && bounds.Height != 0);
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                // todo: localize
                throw new ArgumentException("Bounds of quadrant cannot be zero width or height");
            }

            float w = Bounds.Width / 2;
            if (w == 0)
            {
                w = 1;
            }
            float h = Bounds.Height / 2;
            if (h == 0)
            {
                h = 1;
            }

            // assumption that the Rect struct is almost as fast as doing the operations
            // manually since Rect is a value type.

            RectangleF topLeft = new RectangleF(Bounds.Left, Bounds.Top, w, h);
            RectangleF topRight = new RectangleF(Bounds.Left + w, Bounds.Top, w, h);
            RectangleF bottomLeft = new RectangleF(Bounds.Left, Bounds.Top + h, w, h);
            RectangleF bottomRight = new RectangleF(Bounds.Left + w, Bounds.Top + h, w, h);

            IMesQuadrant<T> child = null;

            // See if any child quadrants completely contain this node.
            if (topLeft.Contains(bounds))
            {
                if (TopLeft == null)
                {
                    TopLeft = new MesQuadrant<T>(this, topLeft);
                }
                child = TopLeft;
            }
            else if (topRight.Contains(bounds))
            {
                if (TopRight == null)
                {
                    TopRight = new MesQuadrant<T>(this, topRight);
                }
                child = TopRight;
            }
            else if (bottomLeft.Contains(bounds))
            {
                if (BottomLeft == null)
                {
                    BottomLeft = new MesQuadrant<T>(this, bottomLeft);
                }
                child = BottomLeft;
            }
            else if (bottomRight.Contains(bounds))
            {
                if (BottomRight == null)
                {
                    BottomRight = new MesQuadrant<T>(this, bottomRight);
                }
                child = BottomRight;
            }

            if (child != null)
            {
                return child.Insert(node, bounds);
            }

            MesQuadNode<T> n = new MesQuadNode<T>(node, bounds);
            if (Nodes == null)
            {
                n.Next = n;
            }
            else
            {
                // link up in circular link list.
                IMesQuadNode<T> x = Nodes;
                n.Next = x.Next;
                x.Next = n;
            }
            Nodes = n;
            return this;
        }
        /// <inheritdoc />
        public void GetIntersectingNodes(IList<IMesQuadNode<T>> nodes, RectangleF bounds)
        {
            if (bounds.IsEmpty) { return; }
            float w = Bounds.Width / 2;
            float h = Bounds.Height / 2;

            // assumption that the Rect struct is almost as fast as doing the operations
            // manually since Rect is a value type.

            RectangleF topLeft = new RectangleF(Bounds.Left, Bounds.Top, w, h);
            RectangleF topRight = new RectangleF(Bounds.Left + w, Bounds.Top, w, h);
            RectangleF bottomLeft = new RectangleF(Bounds.Left, Bounds.Top + h, w, h);
            RectangleF bottomRight = new RectangleF(Bounds.Left + w, Bounds.Top + h, w, h);

            // See if any child quadrants completely contain this node.
            if (topLeft.IntersectsWith(bounds) && TopLeft != null)
            {
                TopLeft.GetIntersectingNodes(nodes, bounds);
            }

            if (topRight.IntersectsWith(bounds) && TopRight != null)
            {
                TopRight.GetIntersectingNodes(nodes, bounds);
            }

            if (bottomLeft.IntersectsWith(bounds) && BottomLeft != null)
            {
                BottomLeft.GetIntersectingNodes(nodes, bounds);
            }

            if (bottomRight.IntersectsWith(bounds) && BottomRight != null)
            {
                BottomRight.GetIntersectingNodes(nodes, bounds);
            }

            GetIntersectingNodes(Nodes, nodes, bounds);
        }
        /// <inheritdoc />
        public void GetIntersectingNodes(IMesQuadNode<T> last, IList<IMesQuadNode<T>> nodes, RectangleF bounds)
        {
            if (last == null)
            {
                return;
            }

            IMesQuadNode<T> n = last;
            do
            {
                n = n.Next; // first node.
                if (n.Bounds.IntersectsWith(bounds))
                {
                    nodes.Add(n);
                }
            } while (n != last);
        }
        /// <inheritdoc />
        public bool HasIntersectingNodes(RectangleF bounds)
        {
            if (bounds.IsEmpty) { return false; }
            float w = Bounds.Width / 2;
            float h = Bounds.Height / 2;

            // assumption that the Rect struct is almost as fast as doing the operations
            // manually since Rect is a value type.

            RectangleF topLeft = new RectangleF(Bounds.Left, Bounds.Top, w, h);
            RectangleF topRight = new RectangleF(Bounds.Left + w, Bounds.Top, w, h);
            RectangleF bottomLeft = new RectangleF(Bounds.Left, Bounds.Top + h, w, h);
            RectangleF bottomRight = new RectangleF(Bounds.Left + w, Bounds.Top + h, w, h);

            bool found = false;

            // See if any child quadrants completely contain this node.
            if (topLeft.IntersectsWith(bounds) && TopLeft != null)
            {
                found = TopLeft.HasIntersectingNodes(bounds);
            }

            if (!found && topRight.IntersectsWith(bounds) && TopRight != null)
            {
                found = TopRight.HasIntersectingNodes(bounds);
            }

            if (!found && bottomLeft.IntersectsWith(bounds) && BottomLeft != null)
            {
                found = BottomLeft.HasIntersectingNodes(bounds);
            }

            if (!found && bottomRight.IntersectsWith(bounds) && BottomRight != null)
            {
                found = BottomRight.HasIntersectingNodes(bounds);
            }
            if (!found)
            {
                found = HasIntersectingNodes(Nodes, bounds);
            }
            return found;
        }
        /// <inheritdoc />
        public bool HasIntersectingNodes(IMesQuadNode<T> last, RectangleF bounds)
        {
            if (last == null)
            {
                return false;
            }

            IMesQuadNode<T> n = last;
            do
            {
                n = n.Next; // first node.
                if (n.Bounds.IntersectsWith(bounds))
                {
                    return true;
                }
            } while (n != last);
            return false;
        }
        /// <inheritdoc />
        public bool RemoveNode(T node)
        {
            if (Nodes == null)
            {
                return false;
            }

            IMesQuadNode<T> p = Nodes;
            while (!p.Next.Node.Equals(node) && p.Next != Nodes)
            {
                p = p.Next;
            }

            if (!p.Next.Node.Equals(node))
            {
                return false;
            }

            // Remove Node
            IMesQuadNode<T> n = p.Next;
            if (p == n)
            {
                // list goes to empty
                Nodes = null;
            }
            else
            {
                if (Nodes == n) { Nodes = p; }
                p.Next = n.Next;
            }
            return true;
        }
    }
}