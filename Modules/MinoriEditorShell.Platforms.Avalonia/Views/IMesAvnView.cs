using Avalonia;
using Avalonia.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public interface IMesAvnView : IMvxView, IMvxBindingContextOwner, IControl, IStyledElement
    {
    }

    public interface IMesAvnView<TViewModel>
        : IMesAvnView
        , IMvxView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        MvxFluentBindingDescriptionSet<IMesAvnView<TViewModel>, TViewModel> CreateBindingSet();
    }
}
