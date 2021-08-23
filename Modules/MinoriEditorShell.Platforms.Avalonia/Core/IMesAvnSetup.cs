using Avalonia.Controls;
using Avalonia.Threading;
using MinoriEditorShell.Platforms.Avalonia.Presenters;
using MvvmCross.Core;

namespace MinoriEditorShell.Platforms.Avalonia
{
    public interface IMvxAvnSetup : IMvxSetup
    {
        void PlatformInitialize(Dispatcher uiThreadDispatcher, IMesAvnViewPresenter presenter);
        void PlatformInitialize(Dispatcher uiThreadDispatcher, ContentControl root);
    }
}