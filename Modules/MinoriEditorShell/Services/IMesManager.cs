using MvvmCross.ViewModels;
using System;

namespace MinoriEditorShell.Services
{
    public interface IMesManager
    {
        event EventHandler ActiveDocumentChanging;
        event EventHandler ActiveDocumentChanged;

        Boolean ShowFloatingWindowsInTaskbar { get; set; }

        IMesManagerView ManagerView { get; set; }

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
