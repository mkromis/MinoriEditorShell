using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using System;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    public interface IMesCommandService
    {
        MesCommandDefinitionBase GetCommandDefinition(Type commandDefinitionType);
        MesCommand GetCommand(MesCommandDefinitionBase commandDefinition);
        MesTargetableCommand GetTargetableCommand(MesCommand command);
    }
}
