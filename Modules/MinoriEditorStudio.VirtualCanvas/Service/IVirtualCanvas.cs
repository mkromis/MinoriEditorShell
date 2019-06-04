using MinoriEditorStudio.Framework;
using MinoriEditorStudio.VirtualCanvas.Controls;
using MinoriEditorStudio.VirtualCanvas.Gestures;

namespace MinoriEditorStudio.VirtualCanvas.Service
{
    public interface IVirtualCanvas : IDocument
    {
        AutoScroll AutoScroll { get; }
        Controls.VirtualCanvas Graph { get; }
        Pan Pan { get; }
        RectangleSelectionGesture RectZoom { get; }
        MapZoom Zoom { get; }

        void EnsureLoaded();
    }
}