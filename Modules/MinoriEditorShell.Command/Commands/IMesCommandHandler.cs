using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinoriEditorShell.Commands
{
    public interface IMesCommandHandler<TCommandDefinition> : ICommandHandler
        where TCommandDefinition : MesCommandDefinition
    {
        void Update(MesCommand command);
        Task Run(MesCommand command);
    }

    public interface ICommandListHandler<TCommandDefinition> : ICommandHandler
        where TCommandDefinition : MesCommandListDefinition
    {
        void Populate(MesCommand command, List<MesCommand> commands);
        Task Run(MesCommand command);
    }

    public interface ICommandHandler
    {
        
    }

    public interface ICommandListHandler : ICommandHandler
    {
        
    }

    public abstract class CommandHandlerBase<TCommandDefinition> : IMesCommandHandler<TCommandDefinition>
        where TCommandDefinition : MesCommandDefinition
    {
        public virtual void Update(MesCommand command)
        {
            
        }

        public abstract Task Run(MesCommand command);
    }
}
