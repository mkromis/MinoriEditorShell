using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.Shell.Views;

namespace MinoriEditorStudio.Modules.Shell.Services
{
    public interface ILayoutItemStatePersister
    {
        bool SaveState(IShell shell, IShellView shellView, string fileName);
        bool LoadState(IShell shell, IShellView shellView, string fileName);
    }
}
