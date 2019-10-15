using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    public interface IMesMenuBuilder
    {
        void BuildMenuBar(MesMenuBarDefinition menuBarDefinition, MesMenuModel result);
    }
}
