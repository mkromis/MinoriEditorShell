using Avalonia.Input;

namespace MinoriEditorShell.Platforms.Avalonia.ViewModels
{
    internal interface IDropTarget
    {
        void DragOver(System.Object sender, DragEventArgs e);

        void Drop(System.Object sender, DragEventArgs e);
    }
}