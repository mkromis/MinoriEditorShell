using System;
using MvvmCross.Binding;

namespace MinoriEditorShell.Platforms.Avalonia.Binding.MesBinding.Target
{
    public class MesVisibleTargetBinding : MesDependencyPropertyTargetBinding
    {
        public MesVisibleTargetBinding(object target)
            : base(target, "Visibility", UIElement.VisibilityProperty, typeof(Visibility))
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        public override Type TargetType => typeof(bool);

        public override void SetValue(object value)
        {
            if (value == null)
                value = false;
            var boolValue = (bool)value;
            base.SetValue(boolValue ? Visibility.Visible : Visibility.Collapsed);
        }
    }
}
