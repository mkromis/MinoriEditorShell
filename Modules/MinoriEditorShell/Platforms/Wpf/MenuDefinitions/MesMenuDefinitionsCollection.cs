using System.ComponentModel.Composition;
using MinoriEditorShell.Platforms.Wpf.Menus;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection
{
    public static class MesMenuDefinitionsCollection
    {
        [Export]
        public static MesMenuBarDefinition MainMenuBar = new MesMenuBarDefinition();

        [Export]
        public static MesMenuDefinition FileMenu = new MesMenuDefinition(MainMenuBar, 0, Resources.FileMenuText);

        [Export]
        public static MesMenuItemGroupDefinition FileNewOpenMenuGroup = new MesMenuItemGroupDefinition(FileMenu, 0);

        [Export]
        public static MesMenuItemGroupDefinition FileCloseMenuGroup = new MesMenuItemGroupDefinition(FileMenu, 3);

        [Export]
        public static MesMenuItemGroupDefinition FileSaveMenuGroup = new MesMenuItemGroupDefinition(FileMenu, 6);

        [Export]
        public static MesMenuItemGroupDefinition FileExitOpenMenuGroup = new MesMenuItemGroupDefinition(FileMenu, 10);

        [Export]
        public static MesMenuDefinition EditMenu = new MesMenuDefinition(MainMenuBar, 1, Resources.EditMenuText);

        [Export]
        public static MesMenuItemGroupDefinition EditUndoRedoMenuGroup = new MesMenuItemGroupDefinition(EditMenu, 0);

        [Export]
        public static MesMenuDefinition ViewMenu = new MesMenuDefinition(MainMenuBar, 2, Resources.ViewMenuText);

        [Export]
        public static MesMenuItemGroupDefinition ViewToolsMenuGroup = new MesMenuItemGroupDefinition(ViewMenu, 0);

        [Export]
        public static MesMenuItemGroupDefinition ViewPropertiesMenuGroup = new MesMenuItemGroupDefinition(ViewMenu, 100);

        [Export]
        public static MesMenuDefinition ToolsMenu = new MesMenuDefinition(MainMenuBar, 10, Resources.ToolsMenuText);

        [Export]
        public static MesMenuItemGroupDefinition ToolsOptionsMenuGroup = new MesMenuItemGroupDefinition(ToolsMenu, 100);

        [Export]
        public static MesMenuDefinition WindowMenu = new MesMenuDefinition(MainMenuBar, 20, Resources.WindowMenuText);

        [Export]
        public static MesMenuItemGroupDefinition WindowDocumentListMenuGroup = new MesMenuItemGroupDefinition(WindowMenu, 10);

        [Export]
        public static MesMenuDefinition HelpMenu = new MesMenuDefinition(MainMenuBar, 30, Resources.HelpMenuText);
    }
}
