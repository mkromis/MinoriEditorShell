using MinoriEditorShell.Services;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public class PanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ToolTemplate { get; set; }
        public DataTemplate DocumentTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case ITool _:
                   return ToolTemplate;
                case IDocument _:
                    return DocumentTemplate;
                default: 
                    return base.SelectTemplate(item, container);
            }
        }
    }
}
