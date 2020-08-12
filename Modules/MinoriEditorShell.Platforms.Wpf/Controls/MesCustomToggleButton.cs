using System.Windows.Controls.Primitives;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MesCustomToggleButton : ToggleButton
    {
        protected override void OnToggle()
        {
            // Don't update IsChecked - we'll do that with a binding.
        }
    }
}