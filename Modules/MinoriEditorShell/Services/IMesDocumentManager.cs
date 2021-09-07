using MvvmCross.ViewModels;
using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Main interface for interfacing with document manager
    /// </summary>
    public interface IMesDocumentManager
    {
        /// <summary>
        /// Document is about to change
        /// </summary>
        event EventHandler ActiveDocumentChanging;

        /// <summary>
        /// Docuemnt has chagned
        /// </summary>
        event EventHandler ActiveDocumentChanged;

        IMesLayoutItem ActiveItem { get; set; }

        IMesDocument SelectedDocument { get; }

        MvxObservableCollection<IMesDocument> Documents { get; }
        MvxObservableCollection<IMesTool> Tools { get; }

        void ShowTool<TTool>() where TTool : IMesTool;

        void ShowTool(IMesTool model);

        void OpenDocument(IMesDocument model);

        void CloseDocument(IMesDocument document);

        void Close();
    }
}