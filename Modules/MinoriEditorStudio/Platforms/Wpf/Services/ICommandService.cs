using System;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    public interface ICommandService
    {
        CommandDefinitionBase GetCommandDefinition(Type commandDefinitionType);
        Command GetCommand(CommandDefinitionBase commandDefinition);
        TargetableCommand GetTargetableCommand(Command command);
    }
}
