using MinoriEditorStudio.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace MinoriEditorStudio.Modules.Manager.Views
{
    public interface IManagerView
    {
        void LoadLayout(Stream stream, Action<ITool> addToolCallback, Action<IDocument> addDocumentCallback,
                        Dictionary<string, ILayoutItem> itemsState);

        void SaveLayout(Stream stream);

        void UpdateFloatingWindows();
    }
}
