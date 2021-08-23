using MvvmCross.Presenters.Attributes;

namespace MinoriEditorShell.Platforms.Avalonia.Presenters.Attributes
{
    public class MesContentPresentationAttribute : MvxBasePresentationAttribute
    {
        public string WindowIdentifier { get; set; }
        public bool StackNavigation { get; set; } = true;
    }
}