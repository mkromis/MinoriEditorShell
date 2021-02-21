using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Converters
{
    public class MvxNativeValueConverter
        : MarkupExtension, IValueConverter
    {
        private readonly IMvxValueConverter _wrapped;

        public MvxNativeValueConverter(IMvxValueConverter wrapped)
        {
            _wrapped = wrapped;
        }

        protected IMvxValueConverter Wrapped => _wrapped;

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var toReturn = _wrapped.Convert(value, targetType, parameter, culture);
            return MapIfSpecialValue(toReturn);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var toReturn = _wrapped.ConvertBack(value, targetType, parameter, culture);
            return MapIfSpecialValue(toReturn);
        }

        private static object MapIfSpecialValue(object toReturn)
        {
            if (toReturn == MvxBindingConstant.DoNothing)
            {
                throw new NotImplementedException();
                //return System.Windows.Data.Binding.DoNothing;
            }

            if (toReturn == MvxBindingConstant.UnsetValue)
            {
                throw new NotImplementedException();
                //return DependencyProperty.UnsetValue;
            }

            return toReturn;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class MvxNativeValueConverter<T>
        : MvxNativeValueConverter
        where T : IMvxValueConverter, new()
    {
        protected new T Wrapped => (T)base.Wrapped;

        public MvxNativeValueConverter()
            : base(new T())
        {
        }
    }
}
