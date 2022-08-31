using System;

namespace MinoriEditorShell.Platforms.Wpf.Results
{
#warning IResult

    public class MesShowCommonDialogResult /*: IResult*/
    {
#warning ResultCompletionEventArgs

        public event EventHandler</*ResultCompletionEventArgs*/ object> Completed;

#warning fix commmon dialog
        //private readonly CommonDialog _commonDialog;

        //      public MesShowCommonDialogResult(CommonDialog commonDialog)
        //{
        //	_commonDialog = commonDialog;
        //}

#warning CoroutineExecutionContext

        public void Execute(/*CoroutineExecutionContext*/object context)
        {
            throw new NotImplementedException();
            //var result = _commonDialog.ShowDialog().GetValueOrDefault(false);
            //Completed(this, new ResultCompletionEventArgs { WasCancelled = !result });
        }
    }
}