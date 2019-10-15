using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MesToolBarTrayContainer : ContentControl
    {
        static MesToolBarTrayContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MesToolBarTrayContainer),
                new FrameworkPropertyMetadata(typeof(MesToolBarTrayContainer)));
        } 
    }
}
