using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MesMainToolBar : MesToolBarBase
    {
        public MesMainToolBar()
        {
            SetOverflowMode(this, OverflowMode.Always);
            SetResourceReference(StyleProperty, typeof(ToolBar));
        }
    }
}
