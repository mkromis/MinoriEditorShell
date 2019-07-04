using System;
using System.Collections.Generic;
using System.IO;

namespace MinoriEditorStudio.Services
{
    public interface IManagerView
    {
        void LoadLayout(
            Stream stream, Action<ITool> addToolCallback, 
            Action<IDocument> addDocumentCallback,
            Dictionary<String, ILayoutItem> itemsState);

        void SaveLayout(Stream stream);

        void UpdateFloatingWindows();
    }
}
