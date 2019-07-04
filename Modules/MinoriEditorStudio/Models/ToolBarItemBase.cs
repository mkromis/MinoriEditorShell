using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Models
{
	public class ToolBarItemBase : MvxNotifyPropertyChanged
	{
        public static ToolBarItemBase Separator => new ToolBarItemSeparator();

        public virtual string Name => "-";
    }
}
