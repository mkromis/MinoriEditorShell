using System.Collections.Generic;
using System.Windows;
using MvvmCross.Base;
using MvvmCross.Exceptions;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings;
using MvvmCross;
using Avalonia;
using System;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    // ReSharper disable InconsistentNaming
    public static class Bi
    // ReSharper restore InconsistentNaming
    {
        static Bi()
        {
            MesDesignTimeChecker.Check();
        }

        // ReSharper disable InconsistentNaming
#warning fix ndProperty
        //public static readonly AvaloniaProperty ndProperty =
        //    // ReSharper restore InconsistentNaming
        //    AvaloniaProperty.RegisterAttached("nd",
        //                                        typeof(string),
        //                                        typeof(Bi),
        //                                        new AvaloniaProperty(null, CallBackWhenndIsChanged));

        public static string Getnd(AvaloniaProperty obj)
        {
            //return obj.GetValue(ndProperty) as string;
            throw new NotImplementedException();
        }

#warning fix Setnd
        //public static void Setnd(
        //    StyledProperty obj,
        //    string value)
        //{
        //    obj.SetValue(ndProperty, value);
        //}

        private static IMesBindingCreator _bindingCreator;

        private static IMesBindingCreator BindingCreator
        {
            get
            {
                _bindingCreator = _bindingCreator ?? ResolveBindingCreator();
                return _bindingCreator;
            }
        }

        private static IMesBindingCreator ResolveBindingCreator()
        {
            IMesBindingCreator toReturn;
            if (!Mvx.IoCProvider.TryResolve<IMesBindingCreator>(out toReturn))
            {
                throw new MvxException("Unable to resolve the binding creator - have you initialized Windows Binding");
            }

            return toReturn;
        }

        private static void CallBackWhenndIsChanged(
            object sender,
            object args)
            //StyledPropertyChangedEventArgs args)
        {
            //var bindingCreator = BindingCreator;

            //bindingCreator?.CreateBindings(sender, args, ParseBindingDescriptions);
            throw new NotImplementedException();
        }

        private static IEnumerable<MvxBindingDescription> ParseBindingDescriptions(string bindingText)
        {
            throw new NotImplementedException();
            //if (MvxSingleton<IMvxBindingSingletonCache>.Instance == null)
            //    return null;

            //return MvxSingleton<IMvxBindingSingletonCache>.Instance.BindingDescriptionParser.Parse(bindingText);
        }
    }
}
