namespace MinoriEditorShell.Commands
{
    public interface IMesCommandUiItem
    {
        MesCommandDefinitionBase CommandDefinition { get; }
        void Update(MesCommandHandlerWrapper commandHandler);
    }
}
