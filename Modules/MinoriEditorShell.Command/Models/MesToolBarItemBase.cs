using MvvmCross.ViewModels;

namespace MinoriEditorShell.Models
{
	public class MesToolBarItemBase : MvxNotifyPropertyChanged
	{
        public static MesToolBarItemBase Separator => new MesToolBarItemSeparator();

        public virtual string Name => "-";
    }
}
