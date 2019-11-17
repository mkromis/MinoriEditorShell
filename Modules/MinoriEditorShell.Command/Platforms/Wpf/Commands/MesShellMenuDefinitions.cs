using System.ComponentModel.Composition;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;
using MinoriEditorShell.Platforms.Wpf.Menus;
using MinoriEditorShell.Properties;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public static class MesMenuDefinitions
    {
        [Export]
        public static MesMenuItemDefinition FileNewMenuItem = new MesTextMenuItemDefinition(
            MesMenuDefinitionsCollection.FileNewOpenMenuGroup, 0, Resources.FileNewCommandText);

        [Export]
        public static MesMenuItemGroupDefinition FileNewCascadeGroup = new MesMenuItemGroupDefinition(
            FileNewMenuItem, 0);

        [Export]
        public static MesMenuItemDefinition FileNewMenuItemList = new MesCommandMenuItemDefinition<MesNewFileCommandListDefinition>(
            FileNewCascadeGroup, 0);

        [Export]
        public static MesMenuItemDefinition FileOpenMenuItem = new MesCommandMenuItemDefinition<MesOpenFileCommandDefinition>(
            MesMenuDefinitionsCollection.FileNewOpenMenuGroup, 1);

        [Export]
        public static MesMenuItemDefinition FileCloseMenuItem = new MesCommandMenuItemDefinition<MesCloseFileCommandDefinition>(
            MesMenuDefinitionsCollection.FileCloseMenuGroup, 0);

        [Export]
        public static MesMenuItemDefinition FileSaveMenuItem = new MesCommandMenuItemDefinition<MesSaveFileCommandDefinition>(
            MesMenuDefinitionsCollection.FileSaveMenuGroup, 0);

        [Export]
        public static MesMenuItemDefinition FileSaveAsMenuItem = new MesCommandMenuItemDefinition<MesSaveFileAsCommandDefinition>(
            MesMenuDefinitionsCollection.FileSaveMenuGroup, 1);

        [Export]
        public static MesMenuItemDefinition FileSaveAllMenuItem = new MesCommandMenuItemDefinition<MesSaveAllFilesCommandDefinition>(
            MesMenuDefinitionsCollection.FileSaveMenuGroup, 1);

        [Export]
        public static MesMenuItemDefinition FileExitMenuItem = new MesCommandMenuItemDefinition<MesExitCommandDefinition>(
            MesMenuDefinitionsCollection.FileExitOpenMenuGroup, 0);

        [Export]
        public static MesMenuItemDefinition WindowDocumentList = new MesCommandMenuItemDefinition<MesSwitchToDocumentCommandListDefinition>(
            MesMenuDefinitionsCollection.WindowDocumentListMenuGroup, 0);

        [Export]
        public static MesMenuItemDefinition ViewFullscreenItem = new MesCommandMenuItemDefinition<ViewFullScreenCommandDefinition>(
            MesMenuDefinitionsCollection.ViewPropertiesMenuGroup, 0);
    }
}
