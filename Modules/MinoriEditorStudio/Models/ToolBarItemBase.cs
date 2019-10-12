using MvvmCross.ViewModels;

namespace MinoriEditorShell.Models
{
	public class ToolBarItemBase : MvxNotifyPropertyChanged
	{
        public static ToolBarItemBase Separator => new ToolBarItemSeparator();

        public virtual string Name => "-";
    }
}
