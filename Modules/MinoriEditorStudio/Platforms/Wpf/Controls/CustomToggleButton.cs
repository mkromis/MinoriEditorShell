using System.Windows.Controls.Primitives;

namespace MinoriEditorStudio.Platforms.Wpf.Controls
{
    public class CustomToggleButton : ToggleButton
    {
        protected override void OnToggle()
        {
            // Don't update IsChecked - we'll do that with a binding.
        }
    }
}
