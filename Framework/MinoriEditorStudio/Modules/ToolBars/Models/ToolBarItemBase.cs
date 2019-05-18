using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Modules.ToolBars.Models
{
	public class ToolBarItemBase : MvxNotifyPropertyChanged
	{
        public static ToolBarItemBase Separator => new ToolBarItemSeparator();

        public virtual string Name => "-";
    }
}
