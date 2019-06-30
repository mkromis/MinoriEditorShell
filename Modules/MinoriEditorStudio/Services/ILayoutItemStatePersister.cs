using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Services;
using System;

namespace MinoriEditorStudio.Modules.Manager.Services
{
    public interface ILayoutItemStatePersister
    {
        bool SaveState(IManager shell, IManagerView shellView, String fileName);
        bool LoadState(IManager shell, IManagerView shellView, String fileName);
    }
}
