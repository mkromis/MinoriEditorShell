using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class ToolPaneToolBar : ToolBarBase
    {
        static ToolPaneToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolPaneToolBar),
                new FrameworkPropertyMetadata(typeof(ToolPaneToolBar)));
        }

        public ToolPaneToolBar()
        {
            SetOverflowMode(this, OverflowMode.AsNeeded);
            ToolBarTray.SetIsLocked(this, true);
        }
    }
}
