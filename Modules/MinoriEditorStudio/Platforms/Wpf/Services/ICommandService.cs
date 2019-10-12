using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using System;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    public interface ICommandService
    {
        CommandDefinitionBase GetCommandDefinition(Type commandDefinitionType);
        Command GetCommand(CommandDefinitionBase commandDefinition);
        TargetableCommand GetTargetableCommand(Command command);
    }
}
