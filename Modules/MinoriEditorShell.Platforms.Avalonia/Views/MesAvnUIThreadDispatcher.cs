using Avalonia.Threading;
using MvvmCross.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public class MesAvnUIThreadDispatcher
        : MvxMainThreadAsyncDispatcher
    {
        private readonly Dispatcher _dispatcher;

        public MesAvnUIThreadDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public override bool IsOnMainThread => _dispatcher.CheckAccess();

        public override bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            if (IsOnMainThread)
            {
                ExceptionMaskedAction(action, maskExceptions);
            }
            else
            {
                _dispatcher.InvokeAsync(() =>
                {
                    ExceptionMaskedAction(action, maskExceptions);
                });
            }

            // TODO - why return bool at all?
            return true;
        }
    }
}
