using System.ComponentModel.Composition;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;
using MinoriEditorShell.Platforms.Wpf.Menus;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition FileNewMenuItem = new TextMenuItemDefinition(
            MenuDefinitionsCollection.FileNewOpenMenuGroup, 0, Resources.FileNewCommandText);

        [Export]
        public static MenuItemGroupDefinition FileNewCascadeGroup = new MenuItemGroupDefinition(
            FileNewMenuItem, 0);

        [Export]
        public static MenuItemDefinition FileNewMenuItemList = new CommandMenuItemDefinition<NewFileCommandListDefinition>(
            FileNewCascadeGroup, 0);

        [Export]
        public static MenuItemDefinition FileOpenMenuItem = new CommandMenuItemDefinition<OpenFileCommandDefinition>(
            MenuDefinitionsCollection.FileNewOpenMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileCloseMenuItem = new CommandMenuItemDefinition<CloseFileCommandDefinition>(
            MenuDefinitionsCollection.FileCloseMenuGroup, 0);

        [Export]
        public static MenuItemDefinition FileSaveMenuItem = new CommandMenuItemDefinition<SaveFileCommandDefinition>(
            MenuDefinitionsCollection.FileSaveMenuGroup, 0);

        [Export]
        public static MenuItemDefinition FileSaveAsMenuItem = new CommandMenuItemDefinition<SaveFileAsCommandDefinition>(
            MenuDefinitionsCollection.FileSaveMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileSaveAllMenuItem = new CommandMenuItemDefinition<SaveAllFilesCommandDefinition>(
            MenuDefinitionsCollection.FileSaveMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileExitMenuItem = new CommandMenuItemDefinition<ExitCommandDefinition>(
            MenuDefinitionsCollection.FileExitOpenMenuGroup, 0);

        [Export]
        public static MenuItemDefinition WindowDocumentList = new CommandMenuItemDefinition<SwitchToDocumentCommandListDefinition>(
            MenuDefinitionsCollection.WindowDocumentListMenuGroup, 0);

        [Export]
        public static MenuItemDefinition ViewFullscreenItem = new CommandMenuItemDefinition<ViewFullScreenCommandDefinition>(
            MenuDefinitionsCollection.ViewPropertiesMenuGroup, 0);
    }
}
