using MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Models;
using MinoriEditorStudio.VirtualCanvas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Extensions
{
    public static class QuadrantExtension
    {
        public static void Dump<T>(this Quadrant<T> source, LogWriter w) where T : class
        {
            w.WriteAttribute("Bounds", source.Bounds.ToString());
            if (source.Nodes != null)
            {
                QuadNode<T> n = source.Nodes;
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

        private static void DumpQuadrant<T>(String label, Quadrant<T> q, LogWriter w) where T : class
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
