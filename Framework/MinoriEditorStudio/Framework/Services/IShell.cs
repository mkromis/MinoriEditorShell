using System;
using System.Collections.Generic;
using MinoriEditorStudio.Modules.MainMenu;
using MinoriEditorStudio.Modules.StatusBar;
using MinoriEditorStudio.Modules.ToolBars;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Framework.Services
{
#warning fix shell close override
    public interface IShell //: IGuardClose, IDeactivate
	{
        event EventHandler ActiveDocumentChanging;
        event EventHandler ActiveDocumentChanged;

        bool ShowFloatingWindowsInTaskbar { get; set; }
        
		IMenu MainMenu { get; }
        IToolBars ToolBars { get; }
		IStatusBar StatusBar { get; }

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
