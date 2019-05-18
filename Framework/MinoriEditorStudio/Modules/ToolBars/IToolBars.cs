using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Modules.ToolBars
{
    public interface IToolBars
    {
        MvxObservableCollection<IToolBar> Items {get;}
        bool Visible { get; set; }
    }
}
