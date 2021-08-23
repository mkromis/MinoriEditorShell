using Avalonia;
using Avalonia.Controls;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public interface IMesAvnViewLoader
    {
        IMesAvnView CreateView(MvxViewModelRequest request);
        IMesAvnView CreateView(Type viewType);
    }
}