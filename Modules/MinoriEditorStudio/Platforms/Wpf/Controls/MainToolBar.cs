using System.Windows.Controls;

namespace MinoriEditorStudio.Platforms.Wpf.Controls
{
    public class MainToolBar : ToolBarBase
    {
        public MainToolBar()
        {
            SetOverflowMode(this, OverflowMode.Always);
            SetResourceReference(StyleProperty, typeof(ToolBar));
        }
    }
}
