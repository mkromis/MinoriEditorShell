using MvvmCross.ViewModels;
using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Manages Documents for main view
    /// </summary>
    public interface IMesDocumentManager
    {
        /// <summary>
        /// Active document is about to change
        /// </summary>
        event EventHandler ActiveDocumentChanging;
        /// <summary>
        /// Active document has changed
        /// </summary>
        event EventHandler ActiveDocumentChanged;
        /// <summary>
        /// To show floating windows in taskbar or not
        /// </summary>
        bool ShowFloatingWindowsInTaskbar { get; set; }
        /// <summary>
        /// Get the manager view
        /// </summary>
        IMesDocumentManagerView ManagerView { get; set; }
        /// <summary>
        /// Get the current layout item
        /// </summary>
        IMesLayoutItem ActiveItem { get; set; }
        /// <summary>
        /// Get current document
        /// </summary>
        IMesDocument SelectedDocument { get; }
        /// <summary>
        /// Collection of all documents
        /// </summary>
        MvxObservableCollection<IMesDocument> Documents { get; }
        /// <summary>
        /// Collection of all tools
        /// </summary>
        MvxObservableCollection<IMesTool> Tools { get; }
        /// <summary>
        /// Show tool window
        /// </summary>
        /// <typeparam name="TTool"></typeparam>
        void ShowTool<TTool>() where TTool : IMesTool;
        /// <summary>
        /// Show tool window
        /// </summary>
        /// <param name="model"></param>
        void ShowTool(IMesTool model);
        /// <summary>
        /// Open document from given model
        /// </summary>
        /// <param name="model"></param>
        void OpenDocument(IMesDocument model);
        /// <summary>
        /// Closes specific document
        /// </summary>
        /// <param name="document"></param>
        void CloseDocument(IMesDocument document);
        /// <summary>
        /// Closes view
        /// </summary>
        void Close();
    }
}