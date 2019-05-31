using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

namespace MinoriEditorStudio.Modules.StatusBar.ViewModels
{
	public class StatusBarViewModel : MvxNotifyPropertyChanged, IStatusBar
	{
        private readonly StatusBarItemCollection _items;
        public ICollection<StatusBarItemViewModel> Items => _items;

        public StatusBarViewModel()
        {
            _items = new StatusBarItemCollection();
        }

	    public void AddItem(string message, GridLength width)
	    {
	        Items.Add(new StatusBarItemViewModel(message, width));
	    }

	    private class StatusBarItemCollection : MvxObservableCollection<StatusBarItemViewModel>
        {
            protected override void InsertItem(int index, StatusBarItemViewModel item)
            {
                item.Index = index;
                base.InsertItem(index, item);
            }

            protected override void SetItem(int index, StatusBarItemViewModel item)
            {
                item.Index = index;
                base.SetItem(index, item);
            }
        }
	}
}
