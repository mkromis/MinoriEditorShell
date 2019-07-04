using MinoriEditorStudio.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorStudio.Platforms.Wpf.Controls
{
	public class PanesStyleSelector : StyleSelector
	{
		public Style ToolStyle { get; set; }
		public Style DocumentStyle { get; set; }

		public override Style SelectStyle(Object item, DependencyObject container)
		{
            switch(item)
            {
                case ITool _:
                    return ToolStyle;
                case IDocument _:
                    return DocumentStyle;
                default:
                    return base.SelectStyle(item, container);
            }
		}
	}
}
