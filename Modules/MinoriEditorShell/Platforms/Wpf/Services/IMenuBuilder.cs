using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    public interface IMenuBuilder
    {
        void BuildMenuBar(MenuBarDefinition menuBarDefinition, MenuModel result);
    }
}
