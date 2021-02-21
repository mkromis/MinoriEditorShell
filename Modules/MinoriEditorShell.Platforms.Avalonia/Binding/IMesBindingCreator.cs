using System;
using System.Collections.Generic;
using System.Windows;
using MvvmCross.Binding.Bindings;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public interface IMesBindingCreator
    {
        void CreateBindings(
            object sender,
            DependencyPropertyChangedEventArgs args,
            Func<string, IEnumerable<MvxBindingDescription>> parseBindingDescriptions);
    }
}
