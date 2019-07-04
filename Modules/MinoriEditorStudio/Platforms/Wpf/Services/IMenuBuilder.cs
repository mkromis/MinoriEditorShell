using MinoriEditorStudio.Models;
using MinoriEditorStudio.Platforms.Wpf.MenuDefinitions;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    public interface IMenuBuilder
    {
        void BuildMenuBar(MenuBarDefinition menuBarDefinition, MenuModel result);
    }
}
