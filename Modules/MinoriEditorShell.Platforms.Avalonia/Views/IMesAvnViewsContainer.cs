using Avalonia;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public interface IMesAvnViewsContainer : IMvxViewsContainer, IMesAvnViewLoader
    {
        IMesAvnView CreateView(MvxViewModelRequest request);
        IMesAvnView CreateView(Type viewType);
    }
}
