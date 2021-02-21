using System;
using System.Windows;
using System.Collections.Generic;
using MvvmCross.Binding.Bindings;
using MvvmCross.Base;
using MvvmCross.Binding;
using MvvmCross.Core;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    // ReSharper disable InconsistentNaming
    public static class La
    // ReSharper restore InconsistentNaming
    {
        static La()
        {
            MesDesignTimeChecker.Check();
        }

        // ReSharper disable InconsistentNaming
        public static readonly DependencyProperty ngProperty =
            // ReSharper restore InconsistentNaming
            DependencyProperty.RegisterAttached("ng",
                                                typeof(string),
                                                typeof(La),
                                                new PropertyMetadata(null, CallBackWhenngIsChanged));

        public static string Getng(DependencyObject obj)
        {
            return obj.GetValue(ngProperty) as string;
        }

        public static void Setng(
            DependencyObject obj,
            string value)
        {
            obj.SetValue(ngProperty, value);
        }

        private static IMesBindingCreator _bindingCreator;

        private static IMesBindingCreator BindingCreator
        {
            get
            {
                _bindingCreator = _bindingCreator ?? Mvx.IoCProvider.Resolve<IMesBindingCreator>();
                return _bindingCreator;
            }
        }

        private static void CallBackWhenngIsChanged(
            object sender,
            DependencyPropertyChangedEventArgs args)
        {
            // bindingCreator may be null in the designer currently
            var bindingCreator = BindingCreator;
            if (bindingCreator == null)
                return;

            bindingCreator.CreateBindings(sender, args, ParseBindingDescriptions);
        }

        private static IEnumerable<MvxBindingDescription> ParseBindingDescriptions(string languageText)
        {
            if (MvxSingleton<IMvxBindingSingletonCache>.Instance == null)
                return null;

            return MvxSingleton<IMvxBindingSingletonCache>.Instance.BindingDescriptionParser.LanguageParse(languageText);
        }
    }
}
