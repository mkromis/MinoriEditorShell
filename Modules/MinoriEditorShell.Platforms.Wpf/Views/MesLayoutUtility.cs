using AvalonDock;
using AvalonDock.Layout;
using AvalonDock.Layout.Serialization;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    internal static class MesLayoutUtility
    {
        public static void SaveLayout(DockingManager manager, Stream stream)
        {
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(manager);
            layoutSerializer.Serialize(stream);
        }

        public static void LoadLayout(
            DockingManager manager, Stream stream, Action<IMesDocument> addDocumentCallback,
            Action<IMesTool> addToolCallback, Dictionary<string, IMesLayoutItem> items)
        {
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(manager);

            layoutSerializer.LayoutSerializationCallback += (s, e) =>
                {
                    if (items.TryGetValue(e.Model.ContentId, out IMesLayoutItem item))
                    {
                        e.Content = item;

                        if (item is IMesTool tool && e.Model is LayoutAnchorable anchorable)
                        {
                            addToolCallback(tool);
                            tool.IsVisible = anchorable.IsVisible;

#warning tool.Activate
                            //if (anchorable.IsActive)
                            //    tool.Activate();

                            tool.IsSelected = e.Model.IsSelected;

                            return;
                        }

                        if (item is IMesDocument document && e.Model is LayoutDocument layoutDocument)
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