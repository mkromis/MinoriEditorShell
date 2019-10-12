using System;

namespace MinoriEditorShell.Services
{
    public interface ILayoutItemStatePersister
    {
        Boolean SaveState(IManager shell, IManagerView shellView, String fileName);
        Boolean LoadState(IManager shell, IManagerView shellView, String fileName);
    }
}
