namespace MinoriEditorShell.Platforms.Avalonia.Binding.MesBinding.Target
{
    public class MesCollapsedTargetBinding : MesVisibleTargetBinding
    {
        public MesCollapsedTargetBinding(object target)
            : base(target)
        {
        }

        public override void SetValue(object value)
        {
            if (value == null)
                value = false;
            var boolValue = (bool)value;
            base.SetValue(!boolValue);
        }
    }
}
