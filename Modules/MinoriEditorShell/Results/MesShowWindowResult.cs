using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.Results
{
#warning Not Implemented
#if false
    public class MesShowWindowResult<TWindow> : MesOpenResultBase<TWindow>
        where TWindow : IMesWindow
    {
#warning Get TWindow
        //private readonly Func<TWindow> _windowLocator = () => IoC.Get<TWindow>();

#warning IWindowManager
        //[Import]
        //public IWindowManager WindowManager { get; set; }

        public MesShowWindowResult()
        {
        }

        public MesShowWindowResult(TWindow window)
        {
#warning WindowLocator
            //_windowLocator = () => window;
        }

#warning CoroutineExecutionContext

        public override void Execute(/*CoroutineExecutionContext*/object context)
        {
#if false
            var window = _windowLocator();

            if (_setData != null)
                _setData(window);

            if (_onConfigure != null)
                _onConfigure(window);

            window.Deactivated += (s, e) =>
            {
                if (!e.WasClosed)
                    return;

                if (_onShutDown != null)
                    _onShutDown(window);

                OnCompleted(null, false);
            };

            WindowManager.ShowWindow(window);
#endif
        }
    }
#endif
}