using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// A persistant layout over item restarts
    /// </summary>
    public interface IMesLayoutItemStatePersister
    {
        /// <summary>
        /// Save the state on exiting
        /// </summary>
        /// <param name="shell"></param>
        /// <param name="shellView"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool SaveState(IMesDocumentManager shell, IMesDocumentManagerView shellView, string fileName);
        /// <summary>
        /// Load the state on load
        /// </summary>
        /// <param name="shell"></param>
        /// <param name="shellView"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool LoadState(IMesDocumentManager shell, IMesDocumentManagerView shellView, string fileName);
    }
}