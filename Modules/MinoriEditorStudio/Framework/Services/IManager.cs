using System;
using System.Collections.Generic;
using MinoriEditorStudio.Modules.MainMenu;
using MinoriEditorStudio.Modules.Manager.Views;
using MinoriEditorStudio.Modules.Shell.Views;
using MinoriEditorStudio.Modules.StatusBar;
using MinoriEditorStudio.Modules.ToolBars;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Framework.Services
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
