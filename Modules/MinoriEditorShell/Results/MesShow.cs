using Microsoft.Win32;
using MinoriEditorShell.Results;
using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.Platforms.Wpf.Results
{
	public static class MesShow
	{
#warning fix common dialog
        //public static MesShowCommonDialogResult CommonDialog(CommonDialog commonDialog) => new MesShowCommonDialogResult(commonDialog);

        public static MesShowToolResult<TTool> Tool<TTool>()
            where TTool : IMesTool => new MesShowToolResult<TTool>();

        public static MesShowToolResult<TTool> Tool<TTool>(TTool tool)
            where TTool : IMesTool => new MesShowToolResult<TTool>(tool);

#warning IWindow
#if false
        //public static MesOpenDocumentResult Document(IMesDocument document) => new MesOpenDocumentResult(document);

        //public static MesOpenDocumentResult Document(String path) => new MesOpenDocumentResult(path);

        //public static MesOpenDocumentResult Document<T>() where T : IMesDocument => new MesOpenDocumentResult(typeof(T));

        public static ShowWindowResult<TWindow> Window<TWindow>()
                where TWindow : IWindow
        {
            return new ShowWindowResult<TWindow>();
        }

        public static ShowWindowResult<TWindow> Window<TWindow>(TWindow window)
            where TWindow : IWindow
        {
            return new ShowWindowResult<TWindow>(window);
        }

        public static ShowDialogResult<TWindow> Dialog<TWindow>()
                where TWindow : IWindow
        {
            return new ShowDialogResult<TWindow>();
        }

        public static ShowDialogResult<TWindow> Dialog<TWindow>(TWindow window)
            where TWindow : IWindow
        {
            return new ShowDialogResult<TWindow>(window);
        }
#endif
    }
}
