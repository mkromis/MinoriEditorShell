using MvvmCross.ViewModels;

namespace MinoriEditorShell.Services
{
    public interface IToolBars
    {
        MvxObservableCollection<IToolBar> Items {get;}
        bool Visible { get; set; }
    }
}
