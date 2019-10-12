namespace MinoriEditorShell.Commands
{
    public interface ICommandRerouter
    {
        object GetHandler(CommandDefinitionBase commandDefinition);
    }
}
