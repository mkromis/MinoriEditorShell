namespace MinoriEditorShell.Commands
{
    public interface IMesCommandRouter
    {
        MesCommandHandlerWrapper GetCommandHandler(MesCommandDefinitionBase commandDefinition);
    }
}
