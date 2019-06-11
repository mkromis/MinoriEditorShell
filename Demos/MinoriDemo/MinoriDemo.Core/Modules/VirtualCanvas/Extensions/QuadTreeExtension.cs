using MinoriDemo.Core.Modules.VirtualCanvas.Models;
using MinoriEditorStudio.VirtualCanvas.Services;

namespace MinoriDemo.Core.Modules.VirtualCanvas.Extensions
{
    public static class QuadTreeExtension
    {
        public static void Dump<T>(this IQuadTree<T> source, LogWriter w) where T : class
        {
            if (source.Root != null)
            {
                source.Root.Dump(w);
            }
        }
    }
}
