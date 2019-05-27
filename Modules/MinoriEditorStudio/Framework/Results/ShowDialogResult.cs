using System;
using System.ComponentModel.Composition;

namespace MinoriEditorStudio.Framework.Results
{
    public class ShowDialogResult<TWindow> : OpenResultBase<TWindow>
        where TWindow : IWindow
    {
#warning Get TWindow
        private readonly Func<TWindow> _windowLocator = null; //() => IoC.Get<TWindow>();

        public ShowDialogResult()
        {
        }

        public ShowDialogResult(TWindow window)
        {
            _windowLocator = () => window;
        }

#warning IWindowManger
        //[Import]
        //public IWindowManager WindowManager { get; set; }

#warning Coroutine
        public override void Execute(/*CoroutineExecutionContext*/Object context)
        {
            throw new NotImplementedException();
#if false
            TWindow window = _windowLocator();

            if (_setData != null)
                _setData(window);

            if (_onConfigure != null)
                _onConfigure(window);

            bool result = WindowManager.ShowDialog(window).GetValueOrDefault();

            if (_onShutDown != null)
                _onShutDown(window);

            OnCompleted(null, !result);
#endif
        }
    }
}
