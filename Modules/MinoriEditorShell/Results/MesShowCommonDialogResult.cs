using System;
using Microsoft.Win32;

namespace MinoriEditorShell.Platforms.Wpf.Results
{
#warning IResult
    public class MesShowCommonDialogResult /*: IResult*/
	{
#warning ResultCompletionEventArgs
        public event EventHandler</*ResultCompletionEventArgs*/ Object> Completed;

#warning fix commmon dialog
        //private readonly CommonDialog _commonDialog;

  //      public MesShowCommonDialogResult(CommonDialog commonDialog)
		//{
		//	_commonDialog = commonDialog;
		//}

#warning CoroutineExecutionContext
        public void Execute(/*CoroutineExecutionContext*/Object context)
		{
            throw new NotImplementedException();
			//var result = _commonDialog.ShowDialog().GetValueOrDefault(false);
			//Completed(this, new ResultCompletionEventArgs { WasCancelled = !result });
		}
	}
}
