using System;
using System.Collections.Generic;
using System.IO;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// The view for managing documents
    /// </summary>
    public interface IMesDocumentManagerView
    {
        /// <summary>
        /// Load the previous layout
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="addToolCallback"></param>
        /// <param name="addDocumentCallback"></param>
        /// <param name="itemsState"></param>
        void LoadLayout(
            Stream stream, Action<IMesTool> addToolCallback,
            Action<IMesDocument> addDocumentCallback,
            Dictionary<string, IMesLayoutItem> itemsState);
        /// <summary>
        /// Save the current layout
        /// </summary>
        /// <param name="stream"></param>
        void SaveLayout(Stream stream);
        /// <summary>
        /// Helper to update windows events
        /// </summary>
        void UpdateFloatingWindows();
    }
}