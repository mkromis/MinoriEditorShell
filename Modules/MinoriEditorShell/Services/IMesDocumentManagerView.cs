using System;
using System.Collections.Generic;
using System.IO;

namespace MinoriEditorShell.Services
{
    public interface IMesDocumentManagerView
    {
        void LoadLayout(
            Stream stream, Action<IMesTool> addToolCallback,
            Action<IMesDocument> addDocumentCallback,
            Dictionary<String, IMesLayoutItem> itemsState);

        void SaveLayout(Stream stream);

        void UpdateFloatingWindows();
    }
}