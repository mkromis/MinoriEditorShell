using MinoriEditorShell.Services;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
#warning Not Implemented
#if false
    public class MesPanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ToolTemplate { get; set; }
        public DataTemplate DocumentTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case IMesTool _:
                    return ToolTemplate;

                case IMesDocument _:
                    return DocumentTemplate;

                default:
                    return base.SelectTemplate(item, container);
            }
        }
    }
#endif
}