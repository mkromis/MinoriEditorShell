using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinoriEditorShell.VirtualCanvas.Services
{
    public interface IMesQuadrant<T>
    {
        IMesQuadrant<T> BottomLeft { get; }
        IMesQuadrant<T> BottomRight { get; }
        RectangleF Bounds { get; }
        IMesQuadNode<T> Nodes { get; }
        IMesQuadrant<T> TopLeft { get; }
        IMesQuadrant<T> TopRight { get; }

        void ShowQuadTree(object c);

        bool HasIntersectingNodes(RectangleF bounds);

        void GetIntersectingNodes(IList<IMesQuadNode<T>> nodes, RectangleF bounds);

        void GetIntersectingNodes(IMesQuadNode<T> last, IList<IMesQuadNode<T>> nodes, RectangleF bounds);

        IMesQuadrant<T> Insert(T node, RectangleF bounds);

        bool RemoveNode(T node);
    }
}