using MinoriEditorStudio.Framework.Menus;
using MinoriEditorStudio.Modules.MainMenu.Models;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    public interface IMenuBuilder
    {
        void BuildMenuBar(MenuBarDefinition menuBarDefinition, MenuModel result);
    }
}
