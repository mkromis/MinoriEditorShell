using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinoriEditorStudio.VirtualCanvas.Services
{
    public interface IQuadrant<T>
    {
        IQuadrant<T> BottomLeft { get; }
        IQuadrant<T> BottomRight { get; }
        RectangleF Bounds { get; }
        IQuadNode<T> Nodes { get; }
        IQuadrant<T> TopLeft { get; }
        IQuadrant<T> TopRight { get; }

        void ShowQuadTree(Object c);
        Boolean HasIntersectingNodes(RectangleF bounds);
        void GetIntersectingNodes(IList<IQuadNode<T>> nodes, RectangleF bounds);
        void GetIntersectingNodes(IQuadNode<T> last, IList<IQuadNode<T>> nodes, RectangleF bounds);
        IQuadrant<T> Insert(T node, RectangleF bounds);
        Boolean RemoveNode(T node);
    }
}