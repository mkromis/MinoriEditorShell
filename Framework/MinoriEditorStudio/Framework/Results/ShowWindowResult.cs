using System;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Framework.Results
{
    public class ShowWindowResult<TWindow> : OpenResultBase<TWindow>
        where TWindow : IWindow
    {
#warning Get TWindow
        //private readonly Func<TWindow> _windowLocator = () => IoC.Get<TWindow>();

#warning IWindowManager
        //[Import]
        //public IWindowManager WindowManager { get; set; }

        public ShowWindowResult()
        {
            
        }

        public ShowWindowResult(TWindow window)
        {
#warning WindowLocator
            //_windowLocator = () => window;
        }

#warning CoroutineExecutionContext
        public override void Execute(/*CoroutineExecutionContext*/Object context)
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
}
