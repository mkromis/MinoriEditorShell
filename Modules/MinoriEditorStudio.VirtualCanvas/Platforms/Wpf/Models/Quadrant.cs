//-----------------------------------------------------------------------
// <copyright file="QuadTree.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MinoriEditorStudio.VirtualCanvas.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MinoriEditorStudio.VirtualCanvas.Models
{

    /// <summary>
    /// The canvas is split up into four Quadrants and objects are stored in the quadrant that contains them
    /// and each quadrant is split up into four child Quadrants recurrsively.  Objects that overlap more than
    /// one quadrant are stored in the _nodes list for this Quadrant.
    /// </summary>
    public class Quadrant<T> : IQuadrant<T>
    {
        /// <summary>
        /// The bounds of this quadrant
        /// </summary>
        public RectangleF Bounds { get; private set; } // quadrant bounds.

        public IQuadNode<T> Nodes { get; private set; } // nodes that overlap the sub quadrant boundaries.

        // The quadrant is subdivided when nodes are inserted that are 
        // completely contained within those subdivisions.
        public IQuadrant<T> TopLeft { get; private set; }
        public IQuadrant<T> TopRight { get; private set; }
        public IQuadrant<T> BottomLeft { get; private set; }
        public IQuadrant<T> BottomRight { get; private set; }

        /// <summary>
        /// Statictial information for rendering use.
        /// </summary>
        /// <param name="c"></param>
        public void ShowQuadTree(Object o)
        {
            if (o is Canvas c)
            {
                //Rectangle r = new Rectangle
                //{
                //    Width = Bounds.Width,
                //    Height = Bounds.Height
                //};

                //Canvas.SetLeft(r, Bounds.);
                //Canvas.SetTop(r, Bounds.Top);
                //r.Stroke = Brushes.DarkRed;
                //r.StrokeThickness = 1;
                //r.StrokeDashArray = new DoubleCollection(new Double[] { 2.0, 3.0 });
                //c.Children.Add(r);

                //TopLeft?.ShowQuadTree(c);
                //TopRight?.ShowQuadTree(c);
                //BottomLeft?.ShowQuadTree(c);
                //BottomRight?.ShowQuadTree(c);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Construct new Quadrant with a given bounds all nodes stored inside this quadrant
        /// will fit inside this bounds.  
        /// </summary>
        /// <param name="parent">The parent quadrant (if any)</param>
        /// <param name="bounds">The bounds of this quadrant</param>
        public Quadrant(IQuadrant<T> parent, RectangleF bounds)
        {
            Parent = parent;
            Debug.Assert(bounds.Width != 0 && bounds.Height != 0);
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                // todo: localize
                throw new ArgumentException("Bounds of quadrant cannot be zero width or height");
            }
            Bounds = bounds;
        }

        /// <summary>
        /// The parent Quadrant or null if this is the root
        /// </summary>
        internal IQuadrant<T> Parent { get; }

        /// <summary>
        /// Insert the given node
        /// </summary>
        /// <param name="node">The node </param>
        /// <param name="bounds">The bounds of that node</param>
        /// <returns></returns>
        public IQuadrant<T> Insert(T node, RectangleF bounds)
        {
            Debug.Assert(bounds.Width != 0 && bounds.Height != 0);
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                // todo: localize
                throw new ArgumentException("Bounds of quadrant cannot be zero width or height");
            }

            Single w = Bounds.Width / 2;
            if (w == 0)
            {
                w = 1;
            }
            Single h = Bounds.Height / 2;
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

            IQuadrant<T> child = null;

            // See if any child quadrants completely contain this node.
            if (topLeft.Contains(bounds))
            {
                if (TopLeft == null)
                {
                    TopLeft = new Quadrant<T>(this, topLeft);
                }
                child = TopLeft;
            }
            else if (topRight.Contains(bounds))
            {
                if (TopRight == null)
                {
                    TopRight = new Quadrant<T>(this, topRight);
                }
                child = TopRight;
            }
            else if (bottomLeft.Contains(bounds))
            {
                if (BottomLeft == null)
                {
                    BottomLeft = new Quadrant<T>(this, bottomLeft);
                }
                child = BottomLeft;
            }
            else if (bottomRight.Contains(bounds))
            {
                if (BottomRight == null)
                {
                    BottomRight = new Quadrant<T>(this, bottomRight);
                }
                child = BottomRight;
            }

            if (child != null)
            {
                return child.Insert(node, bounds);
            }
            else
            {
                QuadNode<T> n = new QuadNode<T>(node, bounds);
                if (Nodes == null)
                {
                    n.Next = n;
                }
                else
                {
                    // link up in circular link list.
                    IQuadNode<T> x = Nodes;
                    n.Next = x.Next;
                    x.Next = n;
                }
                Nodes = n;
                return this;
            }
        }

        /// <summary>
        /// Returns all nodes in this quadrant that intersect the given bounds.
        /// The nodes are returned in pretty much random order as far as the caller is concerned.
        /// </summary>
        /// <param name="nodes">List of nodes found in the given bounds</param>
        /// <param name="bounds">The bounds that contains the nodes you want returned</param>
        public void GetIntersectingNodes(IList<IQuadNode<T>> nodes, RectangleF bounds)
        {
            if (bounds.IsEmpty) { return; }
            Single w = Bounds.Width / 2;
            Single h = Bounds.Height / 2;

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

        /// <summary>
        /// Walk the given linked list of QuadNodes and check them against the given bounds.
        /// Add all nodes that intersect the bounds in to the list.
        /// </summary>
        /// <param name="last">The last QuadNode in a circularly linked list</param>
        /// <param name="nodes">The resulting nodes are added to this list</param>
        /// <param name="bounds">The bounds to test against each node</param>
        public void GetIntersectingNodes(IQuadNode<T> last, IList<IQuadNode<T>> nodes, RectangleF bounds)
        {
            if (last != null)
            {
                IQuadNode<T> n = last;
                do
                {
                    n = n.Next; // first node.
                    if (n.Bounds.IntersectsWith(bounds))
                    {
                        nodes.Add(n);
                    }
                } while (n != last);
            }
        }

        /// <summary>
        /// Return true if there are any nodes in this Quadrant that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>boolean</returns>
        public Boolean HasIntersectingNodes(RectangleF bounds)
        {
            if (bounds.IsEmpty) { return false; }
            Single w = Bounds.Width / 2;
            Single h = Bounds.Height / 2;

            // assumption that the Rect struct is almost as fast as doing the operations
            // manually since Rect is a value type.

            RectangleF topLeft = new RectangleF(Bounds.Left, Bounds.Top, w, h);
            RectangleF topRight = new RectangleF(Bounds.Left + w, Bounds.Top, w, h);
            RectangleF bottomLeft = new RectangleF(Bounds.Left, Bounds.Top + h, w, h);
            RectangleF bottomRight = new RectangleF(Bounds.Left + w, Bounds.Top + h, w, h);

            Boolean found = false;

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

        /// <summary>
        /// Walk the given linked list and test each node against the given bounds/
        /// </summary>
        /// <param name="last">The last node in the circularly linked list.</param>
        /// <param name="bounds">Bounds to test</param>
        /// <returns>Return true if a node in the list intersects the bounds</returns>
        public Boolean HasIntersectingNodes(IQuadNode<T> last, RectangleF bounds)
        {
            if (last != null)
            {
                IQuadNode<T> n = last;
                do
                {
                    n = n.Next; // first node.
                    if (n.Bounds.IntersectsWith(bounds))
                    {
                        return true;
                    }
                } while (n != last);
            }
            return false;
        }

        /// <summary>
        /// Remove the given node from this Quadrant.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>Returns true if the node was found and removed.</returns>
        public Boolean RemoveNode(T node)
        {
            Boolean rc = false;
            if (Nodes != null)
            {
                IQuadNode<T> p = Nodes;
                while (!p.Next.Node.Equals(node) && p.Next != Nodes)
                {
                    p = p.Next;
                }
                if (p.Next.Node.Equals(node))
                {
                    rc = true;
                    IQuadNode<T> n = p.Next;
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
                }
            }
            return rc;
        }

    }
}

