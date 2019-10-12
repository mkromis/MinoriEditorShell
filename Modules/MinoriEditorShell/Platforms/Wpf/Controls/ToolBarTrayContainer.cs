using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class ToolBarTrayContainer : ContentControl
    {
        static ToolBarTrayContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolBarTrayContainer),
                new FrameworkPropertyMetadata(typeof(ToolBarTrayContainer)));
        } 
    }
}
