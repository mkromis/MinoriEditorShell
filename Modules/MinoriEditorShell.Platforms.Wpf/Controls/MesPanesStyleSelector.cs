using MinoriEditorShell.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
#warning Not implemented
#if false
    public class MesPanesStyleSelector : StyleSelector
    {
        public Style ToolStyle { get; set; }
        public Style DocumentStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            switch (item)
            {
                case IMesTool _:
                    return ToolStyle;

                case IMesDocument _:
                    return DocumentStyle;

                default:
                    return base.SelectStyle(item, container);
            }
        }
    }
#endif
}