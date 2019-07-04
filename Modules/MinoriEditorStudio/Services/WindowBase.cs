using MvvmCross.ViewModels;
using System;

namespace MinoriEditorStudio.Services
{
#warning Screen
    public abstract class WindowBase : MvxNotifyPropertyChanged, /*Screen,*/ IWindow
	{
	    public String DisplayName { get; set; }
	}
}
