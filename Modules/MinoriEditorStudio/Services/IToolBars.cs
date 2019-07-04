using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Services
{
    public interface IToolBars
    {
        MvxObservableCollection<IToolBar> Items {get;}
        bool Visible { get; set; }
    }
}
