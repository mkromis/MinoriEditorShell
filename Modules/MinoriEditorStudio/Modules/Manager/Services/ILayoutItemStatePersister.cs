using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.Manager.Views;
using MinoriEditorStudio.Modules.Shell.Views;
using System;

namespace MinoriEditorStudio.Modules.Manager.Services
{
    public interface ILayoutItemStatePersister
    {
        bool SaveState(IManager shell, IManagerView shellView, String fileName);
        bool LoadState(IManager shell, IManagerView shellView, String fileName);
    }
}
