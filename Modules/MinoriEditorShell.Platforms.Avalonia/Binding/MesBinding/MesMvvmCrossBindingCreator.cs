using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings;

namespace MinoriEditorShell.Platforms.Avalonia.Binding.MesBinding
{
    public class MesMvvmCrossBindingCreator : MesBindingCreator
    {
        protected override void ApplyBindings(StyledElement attachedObject,
                                              IEnumerable<MvxBindingDescription> bindingDescriptions)
        {
            var binder = MvxBindingSingletonCache.Instance.Binder;
            var bindingDescriptionList = bindingDescriptions.ToList();
            var bindings = binder.Bind(attachedObject.DataContext, attachedObject, bindingDescriptionList);
            RegisterBindingsForUpdates(attachedObject, bindings);
        }

        private void RegisterBindingsForUpdates(AvaloniaObject attachedObject,
                                                IEnumerable<IMvxUpdateableBinding> bindings)
        {
            if (bindings == null)
                return;

            var bindingsList = GetOrCreateBindingsList(attachedObject);
            foreach (var binding in bindings)
            {
                bindingsList.Add(binding);
            }
        }

        private IList<IMvxUpdateableBinding> GetOrCreateBindingsList(AvaloniaObject attachedObject)
        {
            var existing = attachedObject.GetValue(BindingsListProperty) as IList<IMvxUpdateableBinding>;
            if (existing != null)
                return existing;

            // attach the list
            var newList = new List<IMvxUpdateableBinding>();
            attachedObject.SetValue(BindingsListProperty, newList);

            // create a binding watcher for the list
            var binding = new System.Windows.Data.Binding();
            bool attached = false;
            Action attachAction = () =>
            {
                if (attached)
                    return;
                BindingOperations.SetBinding(attachedObject, DataContextWatcherProperty, binding);
                attached = true;
            };

            Action detachAction = () =>
            {
                if (!attached)
                    return;

                BindingOperations.ClearBinding(attachedObject, DataContextWatcherProperty);
                attached = false;
            };
            attachAction();
            attachedObject.Loaded += (o, args) =>
            {
                attachAction();
            };
            attachedObject.Unloaded += (o, args) =>
            {
                detachAction();
            };

            return newList;
        }

        public static readonly DependencyProperty DataContextWatcherProperty = DependencyProperty.Register(
            "DataContextWatcher",
            typeof(object),
            typeof(AvaloniaObject),
            new PropertyMetadata(null, DataContext_Changed));

        public static object GetDataContextWatcher(DependencyObject d)
        {
            return d.GetValue(DataContextWatcherProperty);
        }

        public static void SetDataContextWatcher(DependencyObject d, string value)
        {
            d.SetValue(DataContextWatcherProperty, value);
        }

        public static readonly DependencyProperty BindingsListProperty = DependencyProperty.Register(
            "BindingsList",
            typeof(IList<IMvxUpdateableBinding>),
            typeof(AvaloniaObject),
            new PropertyMetadata(null));

        public static IList<IMvxUpdateableBinding> GetBindingsList(DependencyObject d)
        {
            return d.GetValue(BindingsListProperty) as IList<IMvxUpdateableBinding>;
        }

        public static void SetBindingsList(DependencyObject d, string value)
        {
            d.SetValue(BindingsListProperty, value);
        }

        private static void DataContext_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = d as AvaloniaObject;

            var bindings = frameworkElement?.GetValue(BindingsListProperty) as IList<IMvxUpdateableBinding>;
            if (bindings == null)
                return;

            foreach (var binding in bindings)
            {
                binding.DataContext = e.NewValue;
            }
        }
    }
}
