using System;
using System.ComponentModel;
using Avalonia;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.Extensions;

namespace MinoriEditorShell.Platforms.Avalonia.Binding.MesBinding.Target
{
    public class MesDependencyPropertyTargetBinding : MvxConvertingTargetBinding
    {
        private readonly string _targetName;
        private readonly AvaloniaProperty _targetDependencyProperty;
        private readonly Type _actualPropertyType;
        private readonly TypeConverter _typeConverter;

        public MesDependencyPropertyTargetBinding(object target, string targetName, AvaloniaProperty targetDependencyProperty, Type actualPropertyType)
            : base(target)
        {
            _targetDependencyProperty = targetDependencyProperty;
            _actualPropertyType = actualPropertyType;
            _targetName = targetName;
            _typeConverter = _actualPropertyType.TypeConverter();

            // if we return TwoWay for ImageSource then we end up in
            // problems with WP7 not doing the auto-conversion
            // see some of my angst in http://stackoverflow.com/questions/16752242/how-does-xaml-create-the-string-to-bitmapimage-value-conversion-when-binding-to/16753488#16753488
            // Note: if we discover other issues here, then we should make a more flexible solution

#warning TODO: fix ImageSource
            //if (_actualPropertyType == typeof(ImageSource))
            //{
            //    _defaultBindingMode = MvxBindingMode.OneWay;
            //}
        }

        public override void SubscribeToEvents()
        {
            //var frameworkElement = Target as FrameworkElement;
            //if (frameworkElement == null)
            //    return;
            //var listenerBinding = new System.Windows.Data.Binding(_targetName)
            //{ Source = frameworkElement };

            //var attachedProperty = DependencyProperty.RegisterAttached(
            //    "ListenAttached" + _targetName + Guid.NewGuid().ToString("N")
            //    , typeof(object)
            //    , typeof(FrameworkElement)
            //    , new PropertyMetadata(null, (s, e) => FireValueChanged(e.NewValue)));
            //frameworkElement.SetBinding(attachedProperty, listenerBinding);
            throw new NotImplementedException();
        }

        public override Type TargetType => _actualPropertyType;

        private MvxBindingMode _defaultBindingMode = MvxBindingMode.TwoWay;
        public override MvxBindingMode DefaultMode => _defaultBindingMode;

        protected virtual object GetValueByReflection()
        {
            throw new NotImplementedException();
            //var target = Target as FrameworkElement;
            //if (target == null)
            //{
            //    MvxBindingLog.Warning("Weak Target is null in {0} - skipping Get", GetType().Name);
            //    return null;
            //}

            //return target.GetValue(_targetDependencyProperty);
        }

        protected override void SetValueImpl(object target, object value)
        {
            throw new NotImplementedException();
            //MvxBindingLog.Trace("Receiving setValue to " + (value ?? ""));
            //var frameworkElement = target as FrameworkElement;
            //if (frameworkElement == null)
            //{
            //    MvxBindingLog.Warning("Weak Target is null in {0} - skipping set", GetType().Name);
            //    return;
            //}

            //frameworkElement.SetValue(_targetDependencyProperty, value);
        }

        protected override object MakeSafeValue(object value)
        {
            if (_actualPropertyType.IsInstanceOfType(value))
                return value;

            if (_typeConverter == null
                || value == null)
                // TODO - is this correct? Do we need to do more here? See #297
                return _actualPropertyType.MakeSafeValue(value);

            if (!_typeConverter.CanConvertFrom(value.GetType()))
                return null; // TODO - is this correct? Do we need to do more here? See #297

            return _typeConverter.ConvertFrom(value);
        }
    }
}
