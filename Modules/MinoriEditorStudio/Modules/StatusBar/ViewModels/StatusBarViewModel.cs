using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

namespace MinoriEditorStudio.Modules.StatusBar.ViewModels
{
	public class StatusBarViewModel : MvxNotifyPropertyChanged, IStatusBar
	{
        public MvxObservableCollection<StatusBarItemViewModel> Items { get; }

        public StatusBarViewModel() => Items = new MvxObservableCollection<StatusBarItemViewModel>();

        public void AddItem(String message, GridLength width) => Items.Add(new StatusBarItemViewModel(message, width));

        //private class StatusBarItemCollection : MvxObservableCollection<StatusBarItemViewModel>
        //{
        //    protected override void InsertItem(Int32 index, StatusBarItemViewModel item)
        //    {
        //        item.Index = index;
        //        base.InsertItem(index, item);
        //    }

        //    protected override void SetItem(Int32 index, StatusBarItemViewModel item)
        //    {
        //        item.Index = index;
        //        base.SetItem(index, item);
        //    }
        //}
	}
}
