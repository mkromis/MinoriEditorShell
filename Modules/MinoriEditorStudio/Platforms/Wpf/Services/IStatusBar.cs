using System;
using System.Collections.Generic;
using System.Windows;
using MinoriEditorStudio.Modules.StatusBar.ViewModels;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Modules.StatusBar
{
	public interface IStatusBar
	{
        MvxObservableCollection<StatusBarItemViewModel> Items { get; }

	    void AddItem(String message, GridLength width);
	}
}
