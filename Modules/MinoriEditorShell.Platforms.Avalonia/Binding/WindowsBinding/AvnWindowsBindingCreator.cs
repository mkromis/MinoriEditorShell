using System;
using System.Collections.Generic;
using MvvmCross.Converters;
using MvvmCross.Logging;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.SourceSteps;
using Avalonia.Data;
using MinoriEditorShell.Platforms.Avalonia.Binding;
using Avalonia.Data.Core;
using Avalonia.Data.Converters;
using MinoriEditorShell.Platforms.Avalonia.Converters;
using Avalonia;
using MvvmCross;
using Microsoft.Extensions.Logging;

namespace MinoriEditorShell.Platforms.Avalonia.Binding.WindowsBinding
{
    public class AvnWindowsBindingCreator : MesBindingCreator
    {
        protected virtual void ApplyBinding(MvxBindingDescription bindingDescription, Type actualType,
                                            StyledElement attachedObject)
        {
            ILogger<AvnWindowsBindingCreator> log = MvxLogHost.GetLog<AvnWindowsBindingCreator>();
            AvaloniaProperty dependencyProperty = actualType.FindDependencyProperty(bindingDescription.TargetName);
            if (dependencyProperty == null)
            {
                log.LogWarning("Dependency property not found for {0}", bindingDescription.TargetName);
                return;
            }

            var property = actualType.FindActualProperty(bindingDescription.TargetName);
            if (property == null)
            {
                log.LogWarning("Property not returned {0} - may cause issues", bindingDescription.TargetName);
            }

            var sourceStep = bindingDescription.Source as MvxPathSourceStepDescription;
            if (sourceStep == null)
            {
                log.LogWarning("Binding description for {0} is not a simple path - Windows Binding cannot cope with this", bindingDescription.TargetName);
                return;
            }

            throw new NotImplementedException();
            //var newBinding = new
            //{
            //    Path = new PropertyPath(sourceStep.SourcePropertyPath),
            //    Mode = ConvertMode(bindingDescription.Mode, property?.PropertyType ?? typeof(object)),
            //    Converter = GetConverter(sourceStep.Converter),
            //    ConverterParameter = sourceStep.ConverterParameter,
            //};

            //BindingOperations.Apply(attachedObject, dependencyProperty, newBinding);
        }

        protected override void ApplyBindings(StyledElement attachedObject,
                                              IEnumerable<MvxBindingDescription> bindingDescriptions)
        {
            var actualType = attachedObject.GetType();
            foreach (var bindingDescription in bindingDescriptions)
            {
                ApplyBinding(bindingDescription, actualType, attachedObject);
            }
        }

        protected static IValueConverter GetConverter(IMvxValueConverter converter)
        {
            if (converter == null)
                return null;

            // TODO - consider caching this wrapper - it is a tiny bit wasteful creating a wrapper for each binding
            return new MvxNativeValueConverter(converter);
        }

        protected static BindingMode ConvertMode(MvxBindingMode mode, Type propertyType)
        {
            switch (mode)
            {
                case MvxBindingMode.Default:
                    // if we return TwoWay for ImageSource then we end up in
                    // problems with WP7 not doing the auto-conversion
                    // see some of my angst in http://stackoverflow.com/questions/16752242/how-does-xaml-create-the-string-to-bitmapimage-value-conversion-when-binding-to/16753488#16753488
                    // Note: if we discover other issues here, then we should make a more flexible solution
#warning fix ImageSource
                    //if (propertyType == typeof(ImageSource))
                    //    return BindingMode.OneWay;

                    return BindingMode.TwoWay;

                case MvxBindingMode.TwoWay:
                    return BindingMode.TwoWay;

                case MvxBindingMode.OneWay:
                    return BindingMode.OneWay;

                case MvxBindingMode.OneTime:
                    return BindingMode.OneTime;

                case MvxBindingMode.OneWayToSource:
                    ILogger log = MvxLogHost.GetLog<AvnWindowsBindingCreator>();
                    log.LogWarning("WinPhone doesn't support OneWayToSource");
                    return BindingMode.TwoWay;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }
    }
}