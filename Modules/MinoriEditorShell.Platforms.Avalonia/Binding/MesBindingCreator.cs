using System;
using System.Collections.Generic;
using System.Windows;
using MvvmCross.Logging;
using MvvmCross.Binding.Bindings;
using Avalonia;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public abstract class MesBindingCreator : IMesBindingCreator
    {
        //public void CreateBindings(object sender, DependencyPropertyChangedEventArgs args,
        //                           Func<string, IEnumerable<MvxBindingDescription>> parseBindingDescriptions)
        //{
        //    var attachedObject = sender as StyledElement;
        //    if (attachedObject == null)
        //    {
        //        IMvxLog _log = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor<MesBindingCreator>();
        //        _log.Warn("Null attached StyledElement seen in Bi.nd binding");
        //        return;
        //    }

        //    var text = args.NewValue as string;
        //    if (string.IsNullOrEmpty(text))
        //        return;

        //    var bindingDescriptions = parseBindingDescriptions(text);
        //    if (bindingDescriptions == null)
        //        return;

        //    ApplyBindings(attachedObject, bindingDescriptions);
        //}

        protected abstract void ApplyBindings(StyledElement attachedObject,
                                              IEnumerable<MvxBindingDescription> bindingDescriptions);
    }
}
