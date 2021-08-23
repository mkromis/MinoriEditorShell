
using MvvmCross.Presenters.Attributes;

namespace MinoriEditorShell.Platforms.Avalonia.Presenters.Attributes
{
    public class MesWindowPresentationAttribute : MvxBasePresentationAttribute
    {
        public string Identifier { get; set; }
        public bool Modal { get; set; }
    }
}