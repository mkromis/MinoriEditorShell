using Microsoft.Win32;
using MinoriEditorStudio.Results;
using MinoriEditorStudio.Services;
using System;

namespace MinoriEditorStudio.Platforms.Wpf.Results
{
	public static class Show
	{
        public static ShowCommonDialogResult CommonDialog(CommonDialog commonDialog) => new ShowCommonDialogResult(commonDialog);

        public static ShowToolResult<TTool> Tool<TTool>()
            where TTool : ITool => new ShowToolResult<TTool>();

        public static ShowToolResult<TTool> Tool<TTool>(TTool tool)
            where TTool : ITool => new ShowToolResult<TTool>(tool);

        public static OpenDocumentResult Document(IDocument document) => new OpenDocumentResult(document);

        public static OpenDocumentResult Document(String path) => new OpenDocumentResult(path);

        public static OpenDocumentResult Document<T>() where T : IDocument => new OpenDocumentResult(typeof(T));

#warning IWindow
#if false
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
