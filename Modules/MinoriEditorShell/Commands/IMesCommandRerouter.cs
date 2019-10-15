namespace MinoriEditorShell.Commands
{
    public interface IMesCommandRerouter
    {
        object GetHandler(MesCommandDefinitionBase commandDefinition);
    }
}
