using MinoriEditorStudio.Models;
using MinoriEditorStudio.Platforms.Wpf.MenuDefinitionCollection;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    public interface IMenuBuilder
    {
        void BuildMenuBar(MenuBarDefinition menuBarDefinition, MenuModel result);
    }
}
