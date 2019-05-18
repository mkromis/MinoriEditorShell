using MinoriEditorStudio.Framework.Menus;
using MinoriEditorStudio.Modules.MainMenu.Models;

namespace MinoriEditorStudio.Modules.MainMenu
{
    public interface IMenuBuilder
    {
        void BuildMenuBar(MenuBarDefinition menuBarDefinition, MenuModel result);
    }
}
