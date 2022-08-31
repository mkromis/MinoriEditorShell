using MinoriDemo.Core.Modules.VirtualCanvas.Models;
using MinoriEditorShell.VirtualCanvas.Services;
using System;

namespace MinoriDemo.Core.Modules.VirtualCanvas.Extensions
{
    public static class QuadrantExtension
    {
        public static void Dump<T>(this IMesQuadrant<T> source, LogWriter w) where T : class
        {
            w.WriteAttribute("Bounds", source.Bounds.ToString());
            if (source.Nodes != null)
            {
                IMesQuadNode<T> n = source.Nodes;
                do
                {
                    n = n.Next; // first node.
                    w.Open("node");
                    w.WriteAttribute("Bounds", n.Bounds.ToString());
                    w.Close();
                } while (n != source.Nodes);
            }

            DumpQuadrant("TopLeft", source.TopLeft, w);
            DumpQuadrant("TopRight", source.TopRight, w);
            DumpQuadrant("BottomLeft", source.BottomLeft, w);
            DumpQuadrant("BottomRight", source.BottomRight, w);
        }

        private static void DumpQuadrant<T>(string label, IMesQuadrant<T> q, LogWriter w) where T : class
        {
            if (q != null)
            {
                w.Open("Quadrant");
                w.WriteAttribute("Name", label);
                q.Dump(w);
                w.Close();
            }
        }
    }
}