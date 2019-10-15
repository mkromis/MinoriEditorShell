using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class MesExpanderEx : Expander
    {
        static MesExpanderEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MesExpanderEx),
                new FrameworkPropertyMetadata(typeof(MesExpanderEx)));
        } 
    }
}
