using System;

namespace MinoriEditorShell.Services
{
    public interface IMesLayoutItemStatePersister
    {
        Boolean SaveState(IMesManager shell, IMesManagerView shellView, String fileName);
        Boolean LoadState(IMesManager shell, IMesManagerView shellView, String fileName);
    }
}
