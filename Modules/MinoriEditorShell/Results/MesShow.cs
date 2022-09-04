using MinoriEditorShell.Services;

namespace MinoriEditorShell.Results
{
    /// <summary>
    /// Future implementation of a pop up window
    /// </summary>
    public static class MesShow
    {
#warning fix common dialog
#warning IWindow
#if false
        //public static MesShowCommonDialogResult CommonDialog(CommonDialog commonDialog) => new MesShowCommonDialogResult(commonDialog);
        
        /// <summary>
        /// Show tool (side window) when 
        /// </summary>
        /// <typeparam name="TTool"></typeparam>
        /// <returns></returns>
        public static MesShowToolResult<TTool> Tool<TTool>() where TTool : IMesTool => new MesShowToolResult<TTool>();
        public static MesShowToolResult<TTool> Tool<TTool>(TTool tool) where TTool : IMesTool => new MesShowToolResult<TTool>(tool);


        public static MesOpenDocumentResult Document(IMesDocument document) => new MesOpenDocumentResult(document);
        public static MesOpenDocumentResult Document(String path) => new MesOpenDocumentResult(path);
        public static MesOpenDocumentResult Document<T>() where T : IMesDocument => new MesOpenDocumentResult(typeof(T));

        public static ShowWindowResult<TWindow> Window<TWindow>() where TWindow : IWindow => new ShowWindowResult<TWindow>();

        public static ShowWindowResult<TWindow> Window<TWindow>(TWindow window) where TWindow : IWindow => new ShowWindowResult<TWindow>(window);

        public static ShowDialogResult<TWindow> Dialog<TWindow>() where TWindow : IWindow => new ShowDialogResult<TWindow>();

        public static ShowDialogResult<TWindow> Dialog<TWindow>(TWindow window) where TWindow : IWindow => new ShowDialogResult<TWindow>(window);
#endif
    }
}