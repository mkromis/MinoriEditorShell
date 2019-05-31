using MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Models;
using MinoriEditorStudio.VirtualCanvas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Extensions
{
    public static class QuadTreeExtension
    {
        public static void Dump<T>(this QuadTree<T> source, LogWriter w) where T : class
        {
            if (source.Root != null)
            {
                source.Root.Dump(w);
            }
        }
    }
}
