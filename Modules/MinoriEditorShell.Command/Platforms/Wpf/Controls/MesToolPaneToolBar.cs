using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MesToolPaneToolBar : MesToolBarBase
    {
        static MesToolPaneToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MesToolPaneToolBar),
                new FrameworkPropertyMetadata(typeof(MesToolPaneToolBar)));
        }

        public MesToolPaneToolBar()
        {
            SetOverflowMode(this, OverflowMode.AsNeeded);
            ToolBarTray.SetIsLocked(this, true);
        }
    }
}
