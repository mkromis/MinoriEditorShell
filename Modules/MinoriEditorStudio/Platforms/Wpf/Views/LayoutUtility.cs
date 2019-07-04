using MinoriEditorStudio.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace MinoriEditorStudio.Platforms.Wpf.Views
{
    internal static class LayoutUtility
    {
        public static void SaveLayout(DockingManager manager, Stream stream)
        {
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(manager);
            layoutSerializer.Serialize(stream);
        }

        public static void LoadLayout(
            DockingManager manager, Stream stream, Action<IDocument> addDocumentCallback,
            Action<ITool> addToolCallback, Dictionary<String, ILayoutItem> items)
        {
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(manager);

            layoutSerializer.LayoutSerializationCallback += (s, e) =>
                {
                    if (items.TryGetValue(e.Model.ContentId, out ILayoutItem item))
                    {
                        e.Content = item;



                        if (item is ITool tool && e.Model is LayoutAnchorable anchorable)
                        {
                            addToolCallback(tool);
                            tool.IsVisible = anchorable.IsVisible;

#warning tool.Activate
                            //if (anchorable.IsActive)
                            //    tool.Activate();

                            tool.IsSelected = e.Model.IsSelected;

                            return;
                        }

                        if (item is IDocument document && e.Model is LayoutDocument layoutDocument)
                        {
                            addDocumentCallback(document);

                            // Nasty hack to get around issue that occurs if documents are loaded from state,
                            // and more documents are opened programmatically.
                            layoutDocument.GetType().GetProperty("IsLastFocusedDocument").SetValue(layoutDocument, false, null);

                            document.IsSelected = layoutDocument.IsSelected;
                            return;
                        }
                    }

                    // Don't create any panels if something went wrong.
                    e.Cancel = true;
                };

            try
            {
                layoutSerializer.Deserialize(stream);
            }
            catch
            {
            }
        }
    }
}
