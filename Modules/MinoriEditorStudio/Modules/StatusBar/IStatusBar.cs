using System.Collections.Generic;
using System.Windows;
using MinoriEditorStudio.Modules.StatusBar.ViewModels;

namespace MinoriEditorStudio.Modules.StatusBar
{
	public interface IStatusBar
	{
        ICollection<StatusBarItemViewModel> Items { get; }

	    void AddItem(string message, GridLength width);
	}
}
