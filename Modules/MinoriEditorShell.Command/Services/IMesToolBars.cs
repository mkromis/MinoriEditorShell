using MvvmCross.ViewModels;

namespace MinoriEditorShell.Services
{
    public interface IMesToolBars
    {
        MvxObservableCollection<IMesToolBar> Items {get;}
        bool Visible { get; set; }
    }
}
