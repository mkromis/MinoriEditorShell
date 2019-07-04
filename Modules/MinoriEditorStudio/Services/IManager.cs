using MvvmCross.ViewModels;
using System;

namespace MinoriEditorStudio.Services
{
    public interface IManager
    {
        event EventHandler ActiveDocumentChanging;
        event EventHandler ActiveDocumentChanged;

        Boolean ShowFloatingWindowsInTaskbar { get; set; }

        IManagerView ManagerView { get; set; }

        ILayoutItem ActiveItem { get; set; }

        IDocument SelectedDocument { get; }

        MvxObservableCollection<IDocument> Documents { get; }
        MvxObservableCollection<ITool> Tools { get; }

        void ShowTool<TTool>() where TTool : ITool;
        void ShowTool(ITool model);

        void OpenDocument(IDocument model);
        void CloseDocument(IDocument document);

        void Close();
    }
}
