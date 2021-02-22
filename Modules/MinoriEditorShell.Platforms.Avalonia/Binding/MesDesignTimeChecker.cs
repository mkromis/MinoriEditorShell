using MvvmCross;
using MvvmCross.Binding.Parse.Binding;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public static class MesDesignTimeChecker
    {
        private static bool _checked;

        public static void Check()
        {
            if (_checked)
                return;

            _checked = true;
            if (!MvxDesignTimeHelper.IsInDesignTime)
                return;

            MvxDesignTimeHelper.Initialize();

            if (!Mvx.IoCProvider.CanResolve<IMvxBindingParser>())
            {
                var builder = new MesWindowsBindingBuilder(MesWindowsBindingBuilder.BindingType.MvvmCross);
                builder.DoRegistration();
            }
        }
    }
}
