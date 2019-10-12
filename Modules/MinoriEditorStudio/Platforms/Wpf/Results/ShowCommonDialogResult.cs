using System;
using Microsoft.Win32;

namespace MinoriEditorShell.Platforms.Wpf.Results
{
#warning IResult
    public class ShowCommonDialogResult /*: IResult*/
	{
#warning ResultCompletionEventArgs
        public event EventHandler</*ResultCompletionEventArgs*/ Object> Completed;

		private readonly CommonDialog _commonDialog;

		public ShowCommonDialogResult(CommonDialog commonDialog)
		{
			_commonDialog = commonDialog;
		}

#warning CoroutineExecutionContext
        public void Execute(/*CoroutineExecutionContext*/Object context)
		{
            throw new NotImplementedException();
			//var result = _commonDialog.ShowDialog().GetValueOrDefault(false);
			//Completed(this, new ResultCompletionEventArgs { WasCancelled = !result });
		}
	}
}
