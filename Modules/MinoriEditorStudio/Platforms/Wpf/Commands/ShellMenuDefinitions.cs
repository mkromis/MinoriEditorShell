using System.ComponentModel.Composition;
using MinoriEditorStudio.Commands;
using MinoriEditorStudio.Modules.Platforms.Wpf.Commands;
using MinoriEditorStudio.Platforms.Wpf.MenuDefinitions;
using MinoriEditorStudio.Platforms.Wpf.Menus;
using MinoriEditorStudio.Properties;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition FileNewMenuItem = new TextMenuItemDefinition(
            MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 0, Resources.FileNewCommandText);

        [Export]
        public static MenuItemGroupDefinition FileNewCascadeGroup = new MenuItemGroupDefinition(
            FileNewMenuItem, 0);

        [Export]
        public static MenuItemDefinition FileNewMenuItemList = new CommandMenuItemDefinition<NewFileCommandListDefinition>(
            FileNewCascadeGroup, 0);

        [Export]
        public static MenuItemDefinition FileOpenMenuItem = new CommandMenuItemDefinition<OpenFileCommandDefinition>(
            MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileCloseMenuItem = new CommandMenuItemDefinition<CloseFileCommandDefinition>(
            MainMenu.MenuDefinitions.FileCloseMenuGroup, 0);

        [Export]
        public static MenuItemDefinition FileSaveMenuItem = new CommandMenuItemDefinition<SaveFileCommandDefinition>(
            MainMenu.MenuDefinitions.FileSaveMenuGroup, 0);

        [Export]
        public static MenuItemDefinition FileSaveAsMenuItem = new CommandMenuItemDefinition<SaveFileAsCommandDefinition>(
            MainMenu.MenuDefinitions.FileSaveMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileSaveAllMenuItem = new CommandMenuItemDefinition<SaveAllFilesCommandDefinition>(
            MainMenu.MenuDefinitions.FileSaveMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileExitMenuItem = new CommandMenuItemDefinition<ExitCommandDefinition>(
            MainMenu.MenuDefinitions.FileExitOpenMenuGroup, 0);

        [Export]
        public static MenuItemDefinition WindowDocumentList = new CommandMenuItemDefinition<SwitchToDocumentCommandListDefinition>(
            MainMenu.MenuDefinitions.WindowDocumentListMenuGroup, 0);

        [Export]
        public static MenuItemDefinition ViewFullscreenItem = new CommandMenuItemDefinition<ViewFullScreenCommandDefinition>(
            MainMenu.MenuDefinitions.ViewPropertiesMenuGroup, 0);
    }
}
