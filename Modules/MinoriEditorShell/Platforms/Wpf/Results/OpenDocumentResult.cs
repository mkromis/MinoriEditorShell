using System;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Results;
using MinoriEditorShell.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Results
{
    public class OpenDocumentResult : OpenResultBase<IDocument>
	{
		private readonly IDocument _editor;
		private readonly Type _editorType;
		private readonly String _path;

        private IManager _shell = Mvx.IoCProvider.Resolve<IManager>();

        public OpenDocumentResult(IDocument editor) => _editor = editor;

        public OpenDocumentResult(String path) => _path = path;

        public OpenDocumentResult(Type editorType) => _editorType = editorType;

        public override void Execute(/*CoroutineExecutionContext*/Object context)
		{
#warning CoroutineExec
#if false
			var editor = _editor ??
				(string.IsNullOrEmpty(_path)
					? (IDocument)IoC.GetInstance(_editorType, null)
					:  GetEditor(_path));

			if (editor == null)
			{
				OnCompleted(null, true);
				return;
			}

			if (_setData != null)
				_setData(editor);

			if (_onConfigure != null)
				_onConfigure(editor);

			editor.Deactivated += (s, e) =>
			{
				if (!e.WasClosed)
					return;

				if (_onShutDown != null)
					_onShutDown(editor);
			};

			_shell.OpenDocument(editor);

			OnCompleted(null, false);
#endif
		}

		private static IDocument GetEditor(string path)
		{
		    return OpenFileCommandHandler.GetEditor(path).Result;
		}
	}
}
