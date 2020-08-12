using System;

namespace MinoriEditorShell.Results
{
    public abstract class MesOpenResultBase<TTarget> : IMesOpenResult<TTarget>
    {
        protected Action<TTarget> _setData;
        protected Action<TTarget> _onConfigure;
        protected Action<TTarget> _onShutDown;

        Action<TTarget> IMesOpenResult<TTarget>.OnConfigure
        {
            get { return _onConfigure; }
            set { _onConfigure = value; }
        }

        Action<TTarget> IMesOpenResult<TTarget>.OnShutDown
        {
            get { return _onShutDown; }
            set { _onShutDown = value; }
        }

        //void IOpenResult<TTarget>.SetData<TData>(TData data)
        //{
        //    _setData = child =>
        //    {
        //        var dataCentric = (IDataCentric<TData>)child;
        //        dataCentric.LoadData(data);
        //    };
        //}

        protected virtual void OnCompleted(Exception exception, bool wasCancelled)
        {
#warning OnCompleted
            throw new NotImplementedException();
            //if (Completed != null)
            //	Completed(this, new ResultCompletionEventArgs { Error = exception, WasCancelled = wasCancelled});
        }

#warning CoRoutine Base

        public abstract void Execute(/*CoroutineExecutionContext*/Object context);

#warning ResultCompletionEventArgs

        public event EventHandler</*ResultCompletionEventArgs*/ Object> Completed;
    }
}