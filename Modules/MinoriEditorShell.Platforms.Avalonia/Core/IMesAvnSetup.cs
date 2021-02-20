using Avalonia.Controls;
using Avalonia.Threading;
using MinoriEditorShell.Platforms.Avalonia.Presenters;
using MvvmCross.Core;

namespace MinoriEditorShell.Platforms.Avalonia
{
    public interface IMvxWpfSetup : IMvxSetup
    {
        void PlatformInitialize(Dispatcher uiThreadDispatcher, IMesAvaViewPresenter presenter);
        void PlatformInitialize(Dispatcher uiThreadDispatcher, ContentControl root);
    }
}